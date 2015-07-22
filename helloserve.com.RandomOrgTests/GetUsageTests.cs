using helloseve.com.RandomOrg;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.RandomOrgTests
{
    [TestClass]
    public class GetUsageTests
    {
        [TestMethod]
        public void RandomOrg_Usage()
        {
            RandomOrgProxy proxy = new RandomOrgProxy("your key here");
            int remaining = proxy.GetUsageLeft();

            Assert.IsTrue(remaining > 0);
        }
    }
}
