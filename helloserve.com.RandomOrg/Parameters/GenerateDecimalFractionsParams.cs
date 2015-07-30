using helloserve.com.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateDecimalFractionsParams : BaseGenerateParams
    {
        public int decimalPlaces { get; set; }

        public GenerateDecimalFractionsParams(int decimalPlaces, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.decimalPlaces = decimalPlaces;
        }
    }
}
