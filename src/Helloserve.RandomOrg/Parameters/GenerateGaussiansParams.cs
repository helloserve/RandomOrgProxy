using Helloserve.RandomOrg.Parameters.Base;

namespace Helloserve.RandomOrg.Parameters
{
    internal class GenerateGaussiansParams : BaseNParams
    {
        public double mean { get; set; }
        public double standardDeviation { get; set; }
        public int significantDigits { get; set; }

        public GenerateGaussiansParams(double mean, double standardDeviation, int significantDigits, int n, string apiKey)
            : base(n, apiKey)
        {
            this.mean = mean;
            this.standardDeviation = standardDeviation;
            this.significantDigits = significantDigits;
        }
    }
}
