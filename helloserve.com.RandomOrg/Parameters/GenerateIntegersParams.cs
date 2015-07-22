using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateIntegersParams : BaseParams
    {
        public int n { get; set; }
        public int min { get; set; }
        public int max { get; set; }

        public GenerateIntegersParams(int n, int min, int max, string apiKey)
            : base(apiKey)
        {
            this.n = n;
            this.min = min;
            this.max = max;
        }
    }
}
