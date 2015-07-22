using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Models
{
    internal class Usage
    {
        public string status { get; set; }
        public string creationTime { get; set; }
        public int bitsLeft { get; set; }
        public int requestsLeft { get; set; }
        public int totalBits { get; set; }
        public int totalRequests { get; set; }
    }
}
