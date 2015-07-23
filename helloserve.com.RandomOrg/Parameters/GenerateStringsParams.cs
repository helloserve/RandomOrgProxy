﻿using helloserve.com.RandomOrg.Parameters.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Parameters
{
    internal class GenerateStringsParams : BaseGenerateParams
    {
        public int n { get; set; }
        public int length { get; set; }
        public string characters { get; set; }

        public GenerateStringsParams(int n, int length, string characters, bool replacement, string apiKey)
            : base(replacement, apiKey)
        {
            this.n = n;
            this.length = length;
            this.characters = characters;
        }
    }
}