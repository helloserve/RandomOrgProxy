namespace Helloserve.RandomOrg.Parameters.Base
{
    internal class BaseParams
    {
        public string apiKey { get; set; }

        public BaseParams(string apiKey)
        {
            this.apiKey = apiKey;
        }
    }
}
