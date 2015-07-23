using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters.Base
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
