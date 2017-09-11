﻿using Helloserve.RandomOrg.Models;
using Helloserve.RandomOrg.Models.Base;
using Helloserve.RandomOrg.Parameters;
using Helloserve.RandomOrg.Parameters.Base;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg
{
    internal class RandomOrgClient : IRandomOrgClient
    {
        private RandomOrgOptions _options;
        private BoxMuller _boxMuller = new BoxMuller();

        private int? _requestsLeft;
        private DateTime _requestTime = DateTime.UtcNow;


        public bool WithReplacement
        {
            get { return _options.WithReplacement; }
            set { _options.WithReplacement = value; }
        }

        public RandomOrgClient(RandomOrgOptions options)
        {
            _options = options;
        }

        public bool CanMakeRequest
        {
            get
            {
                if (!_requestsLeft.HasValue)
                {
                    GetUsageLeft();
                }

                return _requestsLeft > 0;
            }
        }

        public DateTime NextRequestTime
        {
            get { return _requestTime; }
        }

        private T ExecuteTask<T>(Task<T> task)
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException)
            {
                if (task.Exception != null)
                    throw task.Exception.InnerException;
            }

            return task.Result;
        }

        private async Task<TResponse> MakeGeneratePOSTAsync<T, TRequest, TResponse>(BaseRequestRpc<TRequest> requestData)
            where TResponse : GenerateBase<T>
        {
            TResponse result = await MakePOSTAsync<TRequest, TResponse>(requestData);

            _requestsLeft = result.requestsLeft;
            _requestTime = DateTime.UtcNow.AddMilliseconds(result.advisoryDelay);

            return result;
        }

        private async Task<TResponse> MakePOSTAsync<TRequest, TResponse>(BaseRequestRpc<TRequest> requestData)
        {
            int id = (int)DateTime.UtcNow.Ticks;
            requestData.id = id;

            string rpc = JsonConvert.SerializeObject(requestData);
            byte[] rpcBuffer = UTF8Encoding.UTF8.GetBytes(rpc);

            using (MemoryStream requestStream = new MemoryStream(rpcBuffer))
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    HttpContent requestContent = new StreamContent(requestStream);
                    requestContent.Headers.Add("Content-Type", "application/json-rpc");

                    HttpResponseMessage response = await httpClient.PostAsync(_options.Url, requestContent);
                    await response.Content.LoadIntoBufferAsync();
                    rpc = await response.Content.ReadAsStringAsync();
                    BaseResponseRpc<TResponse> responseData = JsonConvert.DeserializeObject<BaseResponseRpc<TResponse>>(rpc);

                    if (response.StatusCode != HttpStatusCode.OK || responseData.error != null)
                    {
                        throw new InvalidOperationException(responseData.error.message);
                    }

                    if (responseData.id != id)
                        throw new InvalidOperationException("Response id does not match request id");

                    return responseData.result;
                }
                catch (Exception ex)
                {
                    throw new RandomOrgRequestException("Error occured making a request to random.org.", ex);
                }
            }
        }

        private async Task<T[]> GenerateAsync<T, TParams>(int count, string method, TParams parameters, Func<T[]> fallback, Func<object, T> processor = null)
        {
            if (processor == null)
                processor = (v) => { return (T)v; };

            if (DateTime.UtcNow > NextRequestTime && CanMakeRequest)
            {
                try
                {
                    T[] randomResult = new T[count];

                    GenerateBase<T> result = await MakeGeneratePOSTAsync<T, TParams, GenerateBase<T>>(new BaseRequestRpc<TParams>(method, parameters));
                    for (int i = 0; i < result.random.data.Length; i++)
                    {
                        randomResult[i] = processor(result.random.data[i]);
                    }
                    return randomResult;
                }
                catch
                {
                    if (!_options.ShouldFallback)
                        throw;
                }
            }
            else if (!_options.ShouldFallback)
                throw new RandomOrgRequestException("Cannot make request at this time. Check NextRequestTime and CanMakeRequest first.");

            return fallback();
        }

        public async Task<int> GetUsageLeftAsync()
        {
            try
            {
                Usage usage = await MakePOSTAsync<BaseParams, Usage>(new BaseRequestRpc<BaseParams>("getUsage", new BaseParams(_options.ApiKey)));
                _requestsLeft = usage.requestsLeft;

                return usage.requestsLeft;
            }
            catch
            {
                if (!_options.ShouldFallback)
                    throw;
            }

            return 0;
        }

        public int GetUsageLeft()
        {
            return Task.Run(() => GetUsageLeftAsync()).Result;
        }

        #region Integer

        public async Task<int> GetIntegerAsync(int min, int max)
        {
            return (await GetIntegersAsync(1, min, max))[0];
        }

        public int GetInteger(int min, int max)
        {
            return GetIntegers(1, min, max)[0];
        }

        public async Task<int[]> GetIntegersAsync(int count, int min, int max)
        {
            return await GetIntegersBaseAsync(count, min, max, IntegerBase.Decimal);
        }

        public int[] GetIntegers(int count, int min, int max)
        {
            return ExecuteTask(GetIntegersAsync(count, min, max));
        }

        #endregion

        #region IntegerBase

        public async Task<int> GetIntegerBaseAsync(int min, int max, IntegerBase integerBase)
        {
            return (await GetIntegersBaseAsync(1, min, max, integerBase))[0];
        }

        public int GetIntegerBase(int min, int max, IntegerBase integerBase)
        {
            return GetIntegersBase(1, min, max, integerBase)[0];
        }

        public async Task<int[]> GetIntegersBaseAsync(int count, int min, int max, IntegerBase integerBase)
        {
            if (min < -1000000000 || min > 1000000000)
                throw new ArgumentOutOfRangeException("min must range from -1e9 to 1e9");

            if (max < -1000000000 || max > 1000000000)
                throw new ArgumentOutOfRangeException("max must range from -1e9 to 1e9");

            if (max < min)
                throw new ArgumentException("max cannot be less than min");

            return await GenerateAsync(count, "generateIntegers", new GenerateIntegersParams(count, min, max, WithReplacement, integerBase, _options.ApiKey),
                () =>
                {
                    int[] randomResult = new int[count];
                    Random random = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < randomResult.Length; i++)
                    {
                        randomResult[i] = random.Next(min, max);
                    }
                    return randomResult;
                },
                processor: (v) => (int)Math.Truncate((double)v));
        }

        public int[] GetIntegersBase(int count, int min, int max, IntegerBase integerBase)
        {
            return ExecuteTask(GetIntegersBaseAsync(count, min, max, integerBase));
        }

        #endregion

        #region Double

        public async Task<double> GetDoubleAsync(int decimalPlaces)
        {
            return (await GetDoublesAsync(1, decimalPlaces))[0];
        }

        public double GetDouble(int decimalPlaces)
        {
            return GetDoubles(1, decimalPlaces)[0];
        }


        public async Task<double[]> GetDoublesAsync(int count, int decimalPlaces)
        {
            if (decimalPlaces < 1 || decimalPlaces > 20)
                throw new ArgumentOutOfRangeException("decimalPlaces must range from 2 to 20");

            return await GenerateAsync(count, "generateDecimalFractions", new GenerateDecimalFractionsParams(count, decimalPlaces, WithReplacement, _options.ApiKey),
                () =>
                {
                    double[] randomResult = new double[count];
                    Random random = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < randomResult.Length; i++)
                    {
                        randomResult[i] = Math.Round(random.NextDouble(), decimalPlaces);
                    }
                    return randomResult;
                });
        }

        public double[] GetDoubles(int count, int decimalPlaces)
        {
            return ExecuteTask(GetDoublesAsync(count, decimalPlaces));
        }

        #endregion

        #region Gaussian

        public async Task<double> GetGaussianAsync()
        {
            return await GetGaussianAsync(0.0D, 1.0D, 20);
        }

        public double GetGaussian()
        {
            return GetGaussian(0.0D, 1.0D, 20);
        }

        public async Task<double> GetGaussianAsync(double mean, double standardDeviation, int significantDigits)
        {
            return (await GetGaussiansAsync(1, mean, standardDeviation, significantDigits))[0];
        }

        public double GetGaussian(double mean, double standardDeviation, int significantDigits)
        {
            return GetGaussians(1, mean, standardDeviation, significantDigits)[0];
        }

        public async Task<double[]> GetGaussiansAsync(int count)
        {
            return await GetGaussiansAsync(count, 0.0D, 1.0D, 20);
        }

        public double[] GetGaussians(int count)
        {
            return GetGaussians(count, 0.0D, 1.0D, 20);
        }

        public async Task<double[]> GetGaussiansAsync(int count, double mean, double standardDeviation, int significantDigits)
        {
            if (mean < -1000000.0 || mean > 1000000.0)
                throw new ArgumentOutOfRangeException("mean must range from -1e6 to 1e6");

            if (standardDeviation < -1000000.0 || standardDeviation > 1000000.0)
                throw new ArgumentOutOfRangeException("standardDeviation must range from -1e6 to +1e6");

            if (significantDigits < 2 || significantDigits > 20)
                throw new ArgumentOutOfRangeException("significantDigits must range from 2 to 20");

            return await GenerateAsync(count, "generateGaussians", new GenerateGaussiansParams(count, mean, standardDeviation, significantDigits, _options.ApiKey),
                () =>
                {
                    double[] randomResult = new double[count];
                    for (int i = 0; i < count; i++)
                    {
                        double val = _boxMuller.GenerateGaussian(mean, standardDeviation);
                        double truncVal = Math.Truncate(val);
                        int remainingDigits = significantDigits - truncVal.ToString().Length;
                        randomResult[i] = Math.Round(val, Math.Max(Math.Min(remainingDigits, 15), 0));
                    }

                    return randomResult;
                });
        }

        public double[] GetGaussians(int count, double mean, double standardDeviation, int significantDigits)
        {
            return ExecuteTask(GetGaussiansAsync(count, mean, standardDeviation, significantDigits));
        }

        #endregion

        #region String

        public async Task<string> GetStringAsync(int length)
        {
            return await GetStringAsync(length, _options.AllowedStringCharacters);
        }

        public string GetString(int length)
        {
            return GetString(length, _options.AllowedStringCharacters);
        }

        public async Task<string> GetStringAsync(int length, char[] characters)
        {
            return await GetStringAsync(length, string.Join("", characters));
        }

        public string GetString(int length, char[] characters)
        {
            return GetString(length, string.Join("", characters));
        }

        public async Task<string> GetStringAsync(int length, string characters)
        {
            return (await GetStringsAsync(1, length, characters))[0];
        }

        public string GetString(int length, string characters)
        {
            return GetStrings(1, length, characters)[0];
        }

        public async Task<string[]> GetStringsAsync(int count, int length)
        {
            return await GetStringsAsync(count, length, _options.AllowedStringCharacters);
        }

        public string[] GetStrings(int count, int length)
        {
            return GetStrings(count, length, _options.AllowedStringCharacters);
        }

        public async Task<string[]> GetStringsAsync(int count, int length, char[] characters)
        {
            return await GetStringsAsync(count, length, string.Join("", characters));
        }

        public string[] GetStrings(int count, int length, char[] characters)
        {
            return GetStrings(count, length, string.Join("", characters));
        }

        public async Task<string[]> GetStringsAsync(int count, int length, string characters)
        {
            if (length < 1 || length > 20)
                throw new ArgumentOutOfRangeException("length must range from 1 to 20");

            if (characters.Length > 80)
                throw new ArgumentOutOfRangeException("characters cannot be more than 80");

            return await GenerateAsync(count, "generateStrings", new GenerateStringsParams(count, length, characters, WithReplacement, _options.ApiKey),
                () =>
                {
                    string[] randomResult = new string[count];

                    Random random = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < count; i++)
                    {
                        StringBuilder randomBuilder = new StringBuilder();
                        for (int j = 0; j < length; j++)
                            randomBuilder.Append(characters[random.Next(characters.Length)]);
                        randomResult[i] = randomBuilder.ToString();
                    }

                    return randomResult;
                });
        }

        public string[] GetStrings(int count, int length, string characters)
        {
            return ExecuteTask(GetStringsAsync(count, length, characters));
        }

        #endregion
    }
}
