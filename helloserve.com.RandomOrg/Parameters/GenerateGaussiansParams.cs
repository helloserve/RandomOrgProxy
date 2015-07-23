using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateGaussiansParams : BaseParams
    {
        public int n { get; set; }
        public int mean { get; set; }
        public int standardDeviation { get; set; }
        public int significantDigits { get; set; }

        public GenerateGaussiansParams(int n, int mean, int standardDeviation, int significantDigits, string apiKey) : base(apiKey)
        {
            this.n = n;
            this.mean = mean;
            this.standardDeviation = standardDeviation;
            this.significantDigits = significantDigits;
        }
    }
}
