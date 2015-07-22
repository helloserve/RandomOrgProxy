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

namespace helloseve.com.RandomOrg
{
    public class RandomOrgProxy
    {
        private string _uri = "https://api.random.org/json-rpc/1/invoke";
        private string _apiKey;

        private int? _requestsLeft;
        private DateTime _requestTime = DateTime.UtcNow;

        private object _requestLock = new object();

        public RandomOrgProxy(string apiKey)
        {
            _apiKey = apiKey;
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

        public int GetUsageLeft()
        {
            Usage usage = MakePOST<BaseParams, Usage>(new BaseRequestRpc<BaseParams>("getUsage", new BaseParams(_apiKey)));
            _requestsLeft = usage.requestsLeft;

            return usage.requestsLeft;
        }

        public int GetInteger(int min, int max)
        {
            if (min < -1000000000 || max > 1000000000)
                throw new ArgumentException("Requested range is not supported. Must be between -1000000000 and +1000000000.");

            lock (_requestLock)
            {
                if (DateTime.UtcNow > NextRequestTime && CanMakeRequest)
                {
                    try
                    {
                        GenerateIntegers result = MakePOST<GenerateIntegersParams, GenerateIntegers>(new BaseRequestRpc<GenerateIntegersParams>("generateIntegers", new GenerateIntegersParams(1, min, max, _apiKey)));
                        _requestsLeft = result.requestsLeft;
                        _requestTime = DateTime.UtcNow.AddMilliseconds(result.advisoryDelay);
                        return (int)Math.Round(result.random.data[0]);
                    }
                    catch { }
                }

                Random random = new Random((int)DateTime.Now.Ticks);
                return random.Next(min, max);
            }
        }

        public int[] GetIntegers(int count, int min, int max)
        {
            if (min < -1000000000 || max > 1000000000)
                throw new ArgumentException("Requested range is not supported. Must be between -1000000000 and +1000000000.");

            lock (_requestLock)
            {
                int[] randomResult = new int[count];

                if (DateTime.UtcNow > NextRequestTime && CanMakeRequest)
                {
                    try
                    {
                        GenerateIntegers result = MakePOST<GenerateIntegersParams, GenerateIntegers>(new BaseRequestRpc<GenerateIntegersParams>("generateIntegers", new GenerateIntegersParams(1, min, max, _apiKey)));
                        _requestsLeft = result.requestsLeft;
                        _requestTime = DateTime.UtcNow.AddMilliseconds(result.advisoryDelay);
                        for (int i = 0; i < result.random.data.Length; i++)
                        {
                            randomResult[i] = (int)Math.Round(result.random.data[i]);
                        }
                        return randomResult;
                    }
                    catch { }
                }

                Random random = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < randomResult.Length; i++)
                {
                    randomResult[i] = random.Next(min, max);
                }
                return randomResult;
            }
        }
    }
}
