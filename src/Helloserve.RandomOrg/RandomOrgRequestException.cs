using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg
{
    public class RandomOrgRequestException : Exception
    {
        public RandomOrgRequestException(string message)
            : base(message) { }

        public RandomOrgRequestException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
