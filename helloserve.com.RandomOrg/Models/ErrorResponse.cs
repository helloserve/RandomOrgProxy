using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg.Models
{
    internal class ErrorResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string[] data { get; set; }
    }
}
