using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Models
{
    internal class GenerateIntegers
    {
        public RandomData random { get; set; }
        public int bitsUsed { get; set; }
        public int bitsLeft { get; set; }
        public int requestsLeft { get; set; }
        public int advisoryDelay { get; set; }
    }
}
