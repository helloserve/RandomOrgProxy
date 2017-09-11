using Helloserve.RandomOrg.Parameters.Base;

namespace Helloserve.RandomOrg.Parameters
{
    internal class GenerateIntegersParams : BaseGenerateParams
    {
        public int min { get; set; }
        public int max { get; set; }
        public IntegerBase integerBase { get; set; }

        public GenerateIntegersParams(int min, int max, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.min = min;
            this.max = max;
            this.integerBase = IntegerBase.Decimal;
        }

        public GenerateIntegersParams(int min, int max, IntegerBase integerBase, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.n = n;
            this.min = min;
            this.max = max;
            this.integerBase = integerBase;
        }
    }
}
