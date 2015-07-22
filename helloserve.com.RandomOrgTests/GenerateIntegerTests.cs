using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloseve.com.RandomOrg;

namespace helloserve.com.RandomOrgTests
{
    [TestClass]
    public class GenerateIntegerTests
    {
        [TestMethod]
        public void RandomOrg_GenerateInteger()
        {
            RandomOrgProxy proxy = new RandomOrgProxy("your key here");
            int result = proxy.GetInteger(10, 50);

            Assert.IsTrue(result >= 10);
            Assert.IsTrue(result <= 50);
        }

        [TestMethod]
        public void RandomOrg_GenerateIntegers()
        {
            RandomOrgProxy proxy = new RandomOrgProxy("your key here");
            int[] result = proxy.GetIntegers(100, 10, 50);

            bool inRange = true;
            for (int i = 0; i < result.Length; i++)
            {
                inRange &= result[i] >= 10;
                inRange &= result[i] <= 50;
            }
            Assert.IsTrue(inRange);
        }    
    }
}
