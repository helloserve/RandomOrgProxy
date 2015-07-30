using helloserve.com.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateStringsParams : BaseGenerateParams
    {
        public int length { get; set; }
        public string characters { get; set; }

        public GenerateStringsParams(int length, string characters, bool replacement, int n, string apiKey)
            : base(replacement, n, apiKey)
        {
            this.length = length;
            this.characters = characters;
        }
    }
}
