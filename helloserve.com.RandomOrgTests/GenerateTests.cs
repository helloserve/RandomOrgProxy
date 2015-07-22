using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloseve.com.RandomOrg;

namespace helloserve.com.RandomOrgTests
{
    [TestClass]
    public class GenerateTests
    {
        [TestMethod]
        public void RandomOrg_GenerateInteger()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            int result = proxy.GetInteger(10, 50);

            Assert.IsTrue(result >= 10);
            Assert.IsTrue(result <= 50);
        }

        [TestMethod]
        public void RandomOrg_GenerateIntegers()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            int[] result = proxy.GetIntegers(100, 10, 50);

            Assert.IsTrue(result.Length == 100);

            bool inRange = true;
            for (int i = 0; i < result.Length; i++)
            {
                inRange &= result[i] >= 10;
                inRange &= result[i] <= 50;
            }
            Assert.IsTrue(inRange);
        }

        [TestMethod]
        public void RandomOrg_GenerateDouble()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double result = proxy.GetDouble(6);

            Assert.IsTrue(result >= 0);
            Assert.IsTrue(result <= 1);

            Assert.IsTrue(result.ToString().Length == 8);
        }

        [TestMethod]
        public void RandomOrg_GenerateDoubles()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double[] result = proxy.GetDoubles(100, 6);

            Assert.IsTrue(result.Length == 100);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= result[i].ToString().Length <= 8;
            }
            Assert.IsTrue(lengthCorrect);
        }
    }
}
