using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helloserve.RandomOrg.Models
{
    internal class RandomData<T>
    {
        public T[] data { get; set; }
        public string completionTime { get; set; }
    }
}
