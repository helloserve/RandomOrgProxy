﻿/*
Copyright 2019 Henk Roux (helloserve Productions)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
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
