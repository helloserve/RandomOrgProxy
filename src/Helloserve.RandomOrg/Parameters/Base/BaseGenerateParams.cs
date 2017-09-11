namespace Helloserve.RandomOrg.Parameters.Base
{
    internal class BaseGenerateParams : BaseNParams
    {
        public bool replacement { get; set; }

        public BaseGenerateParams(bool replacement, int n, string apiKey)
            : base(n, apiKey)
        {
            this.replacement = replacement;
        }
    }
}
