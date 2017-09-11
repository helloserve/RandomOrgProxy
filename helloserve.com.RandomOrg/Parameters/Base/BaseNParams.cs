using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters.Base
{
    internal class BaseNParams : BaseParams
    {
        public int n { get; set; }

        public BaseNParams(int n, string apiKey)
            : base(apiKey)
        {
            this.n = n;
        }
    }
}
