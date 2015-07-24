using helloserve.com.RandomOrg.Models;
using helloserve.com.RandomOrg.Models.Base;
using helloserve.com.RandomOrg.Parameters;
using helloserve.com.RandomOrg.Parameters.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg
{
    public class RandomOrgClient
    {
        private string _uri = "https://api.random.org/json-rpc/1/invoke";
        private string _apiKey;
        private BoxMuller _boxMuller = new BoxMuller();

        private int? _requestsLeft;
        private DateTime _requestTime = DateTime.UtcNow;

        private object _requestLock = new object();

        private char[] _allowedCharacters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                                                         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                                                         '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                                                         '~', '!', '@', '#', '$', '%', '^', '&', '*', '-', '=' };

        /// <summary>
        /// Gets or sets the standard set of characters to use when generating random strings.
        /// </summary>
        public char[] AllowedStringCharacters
        {
            get { return _allowedCharacters; }
            set { _allowedCharacters = value; }
        }

        private bool _fallback = true;

        /// <summary>
        /// Gets or sets the behavior of the client in the case of denied, throttled or HTTP error scenarios. Setting it to false will not fall back to the Random() class, and you will receive exceptions.
        /// </summary>
        public bool ShouldFallback
        {
            get { return _fallback; }
            set { _fallback = value; }
        }

        private bool _replacement = true;

        /// <summary>
        /// Gets or sets a global setting for generation with replacement. Setting it to false means that no sequence of generated results will have the same value twice. The Random.Org default is true, and this setting is not applicable to Gaussians.
        /// </summary>
        public bool WithReplacement
        {
            get { return _replacement; }
            set { _replacement = value; }
        }

        public RandomOrgClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Gets a value indicating if there are enough usages left to make another request. Does not take the avised time delay into account.
        /// </summary>
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

        /// <summary>
        /// Gets a value indicating the next request time advised by Random.Org
        /// </summary>
        public DateTime NextRequestTime
        {
            get { return _requestTime; }
        }

        private TResponse MakeGeneratePOST<T, TRequest, TResponse>(BaseRequestRpc<TRequest> requestData)
            where TResponse : GenerateBase<T>
        {
            TResponse result = MakePOST<TRequest, TResponse>(requestData);

            _requestsLeft = result.requestsLeft;
            _requestTime = DateTime.UtcNow.AddMilliseconds(result.advisoryDelay);

            return result;
        }

        private TResponse MakePOST<TRequest, TResponse>(BaseRequestRpc<TRequest> requestData)
        {
            int id = (int)DateTime.UtcNow.Ticks;
            requestData.id = id;

            string rpc = JsonConvert.SerializeObject(requestData);
            byte[] rpcBuffer = UTF8Encoding.UTF8.GetBytes(rpc);

            HttpWebRequest request = HttpWebRequest.CreateHttp(_uri);
            request.Method = "POST";
            request.ContentType = "application/json-rpc";
            Stream stream = request.GetRequestStream();
            stream.Write(rpcBuffer, 0, rpcBuffer.Length);
            stream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            rpcBuffer = new byte[response.ContentLength];
            stream = response.GetResponseStream();
            stream.Read(rpcBuffer, 0, rpcBuffer.Length);
            stream.Close();
            rpc = UTF8Encoding.UTF8.GetString(rpcBuffer);
            BaseResponseRpc<TResponse> responseData = JsonConvert.DeserializeObject<BaseResponseRpc<TResponse>>(rpc);

            if (responseData.id != id)
                throw new InvalidOperationException("Response id does not match request id");

            if (response.StatusCode != HttpStatusCode.OK || responseData.error != null)
            {
                throw new InvalidOperationException(responseData.error.message);
            }

            return responseData.result;
        }

        private T[] Generate<T, TParams>(int count, string method, TParams parameters, Func<T[]> fallback, Func<object, T> processor = null)
        {
            if (processor == null)
                processor = (v) => { return (T)v; };

            lock (_requestLock)
            {
                if (DateTime.UtcNow > NextRequestTime && CanMakeRequest)
                {
                    try
                    {
                        T[] randomResult = new T[count];

                        GenerateBase<T> result = MakeGeneratePOST<T, TParams, GenerateBase<T>>(new BaseRequestRpc<TParams>(method, parameters));
                        for (int i = 0; i < result.random.data.Length; i++)
                        {
                            randomResult[i] = processor(result.random.data[i]);
                        }
                        return randomResult;
                    }
                    catch
                    {
                        if (!_fallback)
                            throw;
                    }
                }
                else if (!_fallback)
                    throw new RandomOrgRequestException("Cannot make request at this time.");

                return fallback();
            }
        }

        public int GetUsageLeft()
        {
            try
            {
                Usage usage = MakePOST<BaseParams, Usage>(new BaseRequestRpc<BaseParams>("getUsage", new BaseParams(_apiKey)));
                _requestsLeft = usage.requestsLeft;

                return usage.requestsLeft;
            }
            catch
            {
                if (!_fallback)
                    throw;
            }

            return 0;
        }

        /// <summary>
        /// Gets a single random integer value between min and max.
        /// </summary>
        /// <param name="min">The minimum value of the random range. Must be greater than -1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A random integer value.</returns>
        public int GetInteger(int min, int max)
        {
            return GetIntegers(1, min, max)[0];
        }

        /// <summary>
        /// Gets an array of random integers.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="min">The minimum value of the random range. Must be greater than 1e9.</param>
        /// <param name="max">The maximum value of the random range. Must be less than 1e9.</param>
        /// <returns>A integer array of random numbers.</returns>
        public int[] GetIntegers(int count, int min, int max)
        {
            if (min < -1000000000 || min > 1000000000)
                throw new ArgumentOutOfRangeException("min must range from -1e9 to 1e9");

            if (max < -1000000000 || max > 1000000000)
                throw new ArgumentOutOfRangeException("max must range from -1e9 to 1e9");

            if (max < min)
                throw new ArgumentException("max cannot be less than min");

            return Generate<int, GenerateIntegersParams>(count, "generateIntegers", new GenerateIntegersParams(count, min, max, _replacement, _apiKey),
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

        /// <summary>
        /// Gets a single random double value between 0.0 and 1.0.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places for the value. Must range from 1 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        public double GetDouble(int decimalPlaces)
        {
            return GetDoubles(1, decimalPlaces)[0];
        }

        /// <summary>
        /// Gets an array of random double values, between 0.0 and 1.0.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="decimalPlaces">The number of decimal places for each value. Must range from 1 and 20.</param>
        /// <returns>An double array of random values between 0.0 and 1.0.</returns>
        public double[] GetDoubles(int count, int decimalPlaces)
        {
            if (decimalPlaces < 1 || decimalPlaces > 20)
                throw new ArgumentOutOfRangeException("decimalPlaces must range from 2 to 20");

            return Generate<double, GenerateDecimalFractionsParams>(count, "generateDecimalFractions", new GenerateDecimalFractionsParams(count, decimalPlaces, _replacement, _apiKey),
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

        /// <summary>
        /// Returns a single random double value from a standard Gaussian distribution.
        /// </summary>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        public double GetGaussian()
        {
            return GetGaussian(0.0D, 1.0D, 20);
        }

        /// <summary>
        /// Returns a single random double value between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        public double GetGaussian(double mean, double standardDeviation, int significantDigits)
        {
            return GetGaussians(1, mean, standardDeviation, significantDigits)[0];
        }

        /// <summary>
        /// Gets an array of random double values between 0.0 and 1.0 from a standard Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <returns>A double array of random values between 0.0 and 1.0.</returns> 
        public double[] GetGaussians(int count)
        {
            return GetGaussians(count, 0.0D, 1.0D, 20);
        }

        /// <summary>
        /// Returns an array of random double values between 0.0 and 1.0 from a Gaussian distribution.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="mean">The distribution's mean. Must range from -1e6 to 1e6.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must range from -1e6 to 1e6.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must range from 2 to 20.</param>
        /// <returns>A double array of random values.</returns> 
        public double[] GetGaussians(int count, double mean, double standardDeviation, int significantDigits)
        {
            if (mean < -1000000.0 || mean > 1000000.0)
                throw new ArgumentOutOfRangeException("mean must range from -1e6 to 1e6");

            if (standardDeviation < -1000000.0 || standardDeviation > 1000000.0)
                throw new ArgumentOutOfRangeException("standardDeviation must range from -1e6 to +1e6");

            if (significantDigits < 2 || significantDigits > 20)
                throw new ArgumentOutOfRangeException("significantDigits must range from 2 to 20");

            return Generate<double, GenerateGaussiansParams>(count, "generateGaussians", new GenerateGaussiansParams(count, mean, standardDeviation, significantDigits, _apiKey),
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

        /// <summary>
        /// Gets a random string based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>A random string value.</returns>
        public string GetString(int length)
        {
            return GetString(length, AllowedStringCharacters);
        }

        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A array containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        public string GetString(int length, char[] characters)
        {
            return GetString(length, string.Join("", characters));
        }

        /// <summary>
        /// Gets a random string.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="characters">A string containing all the characters allowed in the random string. Maximum amount is 80 characters.</param>
        /// <returns>A random string value.</returns>
        public string GetString(int length, string characters)
        {
            return GetStrings(1, length, characters)[0];
        }

        /// <summary>
        /// Gets a random string array based on the content of the AllowedStringCharacters property.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <returns>A string array of random values.</returns>
        public string[] GetStrings(int count, int length)
        {
            return GetStrings(count, length, AllowedStringCharacters);
        }

        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A array containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        public string[] GetStrings(int count, int length, char[] characters)
        {
            return GetStrings(count, length, string.Join("", characters));
        }

        /// <summary>
        /// Gets a random string array.
        /// </summary>
        /// <param name="count">The length of the array to return.</param>
        /// <param name="length">The length of the strings.</param>
        /// <param name="characters">A string containing all the characters allowed in the random strings. Maximum amount is 80 characters.</param>
        /// <returns>A string array of random values.</returns>
        public string[] GetStrings(int count, int length, string characters)
        {
            if (length < 1 || length > 20)
                throw new ArgumentOutOfRangeException("length must range from 1 to 20");

            if (characters.Length > 80)
                throw new ArgumentOutOfRangeException("characters cannot be more than 80");

            return Generate<string, GenerateStringsParams>(count, "generateStrings", new GenerateStringsParams(count, length, characters, _replacement, _apiKey),
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
    }
}
