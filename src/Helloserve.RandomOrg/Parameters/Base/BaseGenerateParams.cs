namespace Helloserve.RandomOrg.Parameters.Base
{
    internal class BaseGenerateParams : BaseParams
    {
        public bool replacement { get; set; }

        public BaseGenerateParams(bool replacement, string apiKey)
            : base(apiKey)
        {
            this.replacement = replacement;
        }
    }
}
