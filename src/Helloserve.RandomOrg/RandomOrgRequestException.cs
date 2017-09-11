using System;

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
