using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg.Parameters.Base
{
    internal class BaseParams
    {
        public string apiKey { get; set; }

        public BaseParams(string apiKey)
        {
            this.apiKey = apiKey;
        }
    }
}
