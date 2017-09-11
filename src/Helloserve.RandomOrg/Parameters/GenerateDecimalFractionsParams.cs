using Helloserve.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg.Parameters
{
    internal class GenerateDecimalFractionsParams : BaseGenerateParams
    {
        public int n { get; set; }
        public int decimalPlaces { get; set; }

        public GenerateDecimalFractionsParams(int n, int decimalPlaces, bool replacement, string apiKey)
            : base(replacement, apiKey)
        {
            this.n = n;
            this.decimalPlaces = decimalPlaces;
        }
    }
}
