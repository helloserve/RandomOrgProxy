﻿namespace Helloserve.RandomOrg.Models.Base
{
    internal class GenerateBase<T>
    {
        public RandomData<T> random { get; set; }

        public int bitsUsed { get; set; }
        public int bitsLeft { get; set; }
        public int requestsLeft { get; set; }
        public int advisoryDelay { get; set; }
    }
}
