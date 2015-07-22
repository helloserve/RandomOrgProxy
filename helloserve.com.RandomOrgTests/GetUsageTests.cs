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
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            int remaining = proxy.GetUsageLeft();

            Assert.IsTrue(remaining > 0);
        }
    }
}
