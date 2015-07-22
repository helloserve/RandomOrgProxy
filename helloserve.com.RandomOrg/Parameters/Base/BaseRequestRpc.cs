using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters.Base
{
    internal class BaseRequestRpc<T>
    {
        public string jsonrpc { get; set; }
        public string method { get; set; }
        public T @params { get; set; }
        public int id { get; set; }

        public BaseRequestRpc(string method, T @params)
        {
            this.jsonrpc = "2.0";
            this.method = method;
            this.@params = @params;
        }
    }
}
