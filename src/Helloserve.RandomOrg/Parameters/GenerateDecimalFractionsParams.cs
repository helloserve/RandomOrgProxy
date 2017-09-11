using Helloserve.RandomOrg.Parameters.Base;

namespace Helloserve.RandomOrg.Parameters
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
