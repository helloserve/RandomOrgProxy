using Helloserve.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg.Parameters
{
    internal class GenerateIntegersParams : BaseGenerateParams
    {
        public int n { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public IntegerBase integerBase { get; set; }

        public GenerateIntegersParams(int n, int min, int max, bool replacement, string apiKey)
            : base(replacement, apiKey)
        {
            this.n = n;
            this.min = min;
            this.max = max;
            this.integerBase = IntegerBase.Decimal;
        }

        public GenerateIntegersParams(int n, int min, int max, bool replacement, IntegerBase integerBase, string apiKey)
            : base(replacement, apiKey)
        {
            this.n = n;
            this.min = min;
            this.max = max;
            this.integerBase = integerBase;
        }
    }
}
