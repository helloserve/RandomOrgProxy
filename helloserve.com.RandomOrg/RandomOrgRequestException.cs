using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrg
{
    public class RandomOrgRequestException : Exception
    {
        public RandomOrgRequestException(string message)
            : base(message)
        {

        }
    }
}
