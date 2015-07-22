using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateDecimalFractionsParams : BaseParams
    {
        public int n { get; set; }
        public int decimalPlaces { get; set; }

        public GenerateDecimalFractionsParams(int n, int decimalPlaces, string apiKey)
            : base(apiKey)
        {
            this.n = n;
            this.decimalPlaces = decimalPlaces;
        }
    }
}
