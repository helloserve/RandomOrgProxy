using helloserve.com.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateIntegersParams : BaseGenerateParams
    {
        public int min { get; set; }
        public int max { get; set; }

        public GenerateIntegersParams(int min, int max, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.min = min;
            this.max = max;
        }
    }
}
