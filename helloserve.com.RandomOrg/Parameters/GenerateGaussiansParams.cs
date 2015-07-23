using helloserve.com.RandomOrg.Parameters.Base;
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
        public double mean { get; set; }
        public double standardDeviation { get; set; }
        public int significantDigits { get; set; }

        public GenerateGaussiansParams(int n, double mean, double standardDeviation, int significantDigits, string apiKey)
            : base(apiKey)
        {
            this.n = n;
            this.mean = mean;
            this.standardDeviation = standardDeviation;
            this.significantDigits = significantDigits;
        }
    }
}
