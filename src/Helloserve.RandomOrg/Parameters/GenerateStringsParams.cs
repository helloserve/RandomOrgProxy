using Helloserve.RandomOrg.Parameters.Base;

namespace Helloserve.RandomOrg.Parameters
{
    internal class GenerateStringsParams : BaseGenerateParams
    {
        public int length { get; set; }
        public string characters { get; set; }

        public GenerateStringsParams(int length, string characters, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.n = n;
            this.length = length;
            this.characters = characters;
        }
    }
}
