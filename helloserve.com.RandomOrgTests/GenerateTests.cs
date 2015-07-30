using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helloserve.com.RandomOrg;

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
        public void RandomOrg_GenerateInteger_MinOutOfRange()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);

            try
            {
                int result = proxy.GetInteger(int.MinValue, 50);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void RandomOrg_GenerateInteger_MaxOutOfRange()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);

            try
            {
                int result = proxy.GetInteger(10, int.MaxValue);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentOutOfRangeException);
            }
        }

        [TestMethod]
        public void RandomOrg_GenerateInteger_MinMaxSwop()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);

            try
            {
                int result = proxy.GetInteger(50, 10);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
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

            Assert.IsTrue(result.ToString().Length <= 8);
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

        [TestMethod]
        public void RandomOrg_GenerateGaussian_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double result = proxy.GetGaussian();

            Assert.IsTrue(result.ToString().Length <= 22);
        }

        [TestMethod]
        public void RandomOrg_GenerateGaussian_Specific()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double result = proxy.GetGaussian(50.0D, 0.5D, 5);

            Assert.IsTrue(result.ToString().Length <= 7);
        }

        [TestMethod]
        public void RandomOrg_GenerateGaussians_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double[] result = proxy.GetGaussians(100);

            Assert.IsTrue(result.Length == 100);
        }

        [TestMethod]
        public void RandomOrg_GenerateGaussians_Specific()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            double[] result = proxy.GetGaussians(100, 20, 100, 6);

            Assert.IsTrue(result.Length == 100);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= (result[i] > 0 && result[i].ToString().Length <= 8) || (result[i] < 0 && result[i].ToString().Length <= 9);
            }
            Assert.IsTrue(lengthCorrect);
        }

        [TestMethod]
        public void Random_GenerateString_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            string result = proxy.GetString(10);

            Assert.IsTrue(result.ToCharArray().Except(proxy.AllowedStringCharacters).Count() == 0);
        }

        [TestMethod]
        public void Random_GenerateString_Specific()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            char[] allowed = new char[] { '1', '2', '3' };
            string result = proxy.GetString(10, allowed);

            Assert.IsTrue(result.ToCharArray().Except(allowed).Count() == 0);
        }
        
        [TestMethod]
        public void Random_GenerateStrings_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            string[] result = proxy.GetStrings(100, 10);

            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(proxy.AllowedStringCharacters).Count() == 0;
            }            

            Assert.IsTrue(allowedCharacters);
        }

        [TestMethod]
        public void Random_GenerateStrings_Specific()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            char[] allowed = new char[] { '1', '2', '3' };
            string[] result = proxy.GetStrings(100, 10, allowed);

            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(proxy.AllowedStringCharacters).Count() == 0;
            }

            Assert.IsTrue(allowedCharacters);
        }

        [TestMethod]
        public void Random_GenerateGuid_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            Guid guid = proxy.GetGuid();

            Assert.IsTrue(guid.ToString().Length == Guid.Empty.ToString().Length);
        }

        [TestMethod]
        public void Random_GenerateGuids_Standard()
        {
            RandomOrgClient proxy = new RandomOrgClient(Constants.ApiKey);
            Guid[] guids = proxy.GetGuids(100);

            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(guids[i].ToString().Length == Guid.Empty.ToString().Length);
            }            
        }
    }
}
