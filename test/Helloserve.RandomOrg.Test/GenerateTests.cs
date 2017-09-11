using Helloserve.RandomOrg.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Helloserve.RandomOrg.Tests
{
    public class GenerateTests : TestClass
    {
        private IRandomOrgClient _randomOrgClient { get { return ServiceProvider.GetService<IRandomOrgClient>(); } }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return base.ConfigureServices(services).AddRandomOrg(Constants.ApiKey);
        }

        [Fact]
        public void GenerateInteger()
        {
            int result = _randomOrgClient.GetInteger(10, 50);

            Assert.True(result >= 10);
            Assert.True(result <= 50);
        }

        [Fact]
        public async Task GenerateIntegerAsync()
        {
            int result = await _randomOrgClient.GetIntegerAsync(10, 50);

            Assert.True(result >= 10);
            Assert.True(result <= 50);
        }

        [Fact]
        public void GenerateInteger_MinOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _randomOrgClient.GetInteger(int.MinValue, 50));
        }

        [Fact]
        public async Task GenerateIntegerAsync_MinOutOfRange()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _randomOrgClient.GetIntegerAsync(int.MinValue, 50));
        }

        [Fact]
        public void GenerateInteger_MaxOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _randomOrgClient.GetInteger(10, int.MaxValue));
        }

        [Fact]
        public async Task GenerateIntegerAsync_MaxOutOfRange()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _randomOrgClient.GetIntegerAsync(10, int.MaxValue));
        }

        [Fact]
        public void GenerateInteger_MinMaxSwop()
        {
            Assert.Throws<ArgumentException>(() => _randomOrgClient.GetInteger(50, 10));
        }

        [Fact]
        public async Task GenerateIntegerAsync_MinMaxSwop()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _randomOrgClient.GetIntegerAsync(50, 10));
        }

        [Fact]
        public void GenerateIntegers()
        {
            int[] result = _randomOrgClient.GetIntegers(100, 10, 50);

            Assert.Equal(100, result.Length);

            bool inRange = true;
            for (int i = 0; i < result.Length; i++)
            {
                inRange &= result[i] >= 10;
                inRange &= result[i] <= 50;
            }
            Assert.True(inRange);
        }

        [Fact]
        public async Task GenerateIntegersAsync()
        {
            int[] result = await _randomOrgClient.GetIntegersAsync(100, 10, 50);

            Assert.Equal(100, result.Length);

            bool inRange = true;
            for (int i = 0; i < result.Length; i++)
            {
                inRange &= result[i] >= 10;
                inRange &= result[i] <= 50;
            }
            Assert.True(inRange);
        }

        [Fact]
        public void GenerateDouble()
        {
            double result = _randomOrgClient.GetDouble(6);

            Assert.True(result >= 0);
            Assert.True(result <= 1);

            Assert.True(result.ToString().Length <= 8);
        }

        [Fact]
        public async Task GenerateDoubleAsync()
        {
            double result = await _randomOrgClient.GetDoubleAsync(6);

            Assert.True(result >= 0);
            Assert.True(result <= 1);

            Assert.True(result.ToString().Length <= 8);
        }

        [Fact]
        public void GenerateDoubles()
        {
            double[] result = _randomOrgClient.GetDoubles(100, 6);

            Assert.Equal(100, result.Length);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= result[i].ToString().Length <= 8;
            }
            Assert.True(lengthCorrect);
        }

        [Fact]
        public async Task GenerateDoublesAsync()
        {
            double[] result = await _randomOrgClient.GetDoublesAsync(100, 6);

            Assert.Equal(100, result.Length);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= result[i].ToString().Length <= 8;
            }
            Assert.True(lengthCorrect);
        }

        [Fact]
        public void GenerateGaussian_Standard()
        {
            double result = _randomOrgClient.GetGaussian();

            Assert.True(result.ToString().Length <= 22);
        }

        [Fact]
        public async Task GenerateGaussianAsync_Standard()
        {
            double result = await _randomOrgClient.GetGaussianAsync();

            Assert.True(result.ToString().Length <= 22);
        }

        [Fact]
        public void GenerateGaussian_Specific()
        {
            double result = _randomOrgClient.GetGaussian(50.0D, 0.5D, 5);

            Assert.True(result.ToString().Length <= 7);
        }

        [Fact]
        public async Task GenerateGaussianAsync_Specific()
        {
            double result = await _randomOrgClient.GetGaussianAsync(50.0D, 0.5D, 5);

            Assert.True(result.ToString().Length <= 7);
        }

        [Fact]
        public void GenerateGaussians_Standard()
        {
            double[] result = _randomOrgClient.GetGaussians(100);

            Assert.Equal(100, result.Length);
        }

        [Fact]
        public async Task GenerateGaussiansAsync_Standard()
        {
            double[] result = await _randomOrgClient.GetGaussiansAsync(100);

            Assert.Equal(100, result.Length);
        }

        [Fact]
        public void GenerateGaussians_Specific()
        {
            double[] result = _randomOrgClient.GetGaussians(100, 20, 100, 6);

            Assert.Equal(100, result.Length);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= (result[i] > 0 && result[i].ToString().Length <= 8) || (result[i] < 0 && result[i].ToString().Length <= 9);
            }
            Assert.True(lengthCorrect);
        }

        [Fact]
        public async Task GenerateGaussiansAsync_Specific()
        {
            double[] result = await _randomOrgClient.GetGaussiansAsync(100, 20, 100, 6);

            Assert.Equal(100, result.Length);

            bool lengthCorrect = true;
            for (int i = 0; i < result.Length; i++)
            {
                lengthCorrect &= (result[i] > 0 && result[i].ToString().Length <= 8) || (result[i] < 0 && result[i].ToString().Length <= 9);
            }
            Assert.True(lengthCorrect);
        }

        [Fact]
        public void GenerateString_Standard()
        {
            string result = _randomOrgClient.GetString(10);
            RandomOrgOptions options = new RandomOrgOptions();
            Assert.True(result.ToCharArray().Except(options.AllowedStringCharacters).Count() == 0);
        }

        [Fact]
        public async Task GenerateStringAsync_Standard()
        {
            string result = await _randomOrgClient.GetStringAsync(10);
            RandomOrgOptions options = new RandomOrgOptions();
            Assert.True(result.ToCharArray().Except(options.AllowedStringCharacters).Count() == 0);
        }

        [Fact]
        public void GenerateString_Specific()
        {
            char[] allowed = new char[] { '1', '2', '3' };
            string result = _randomOrgClient.GetString(10, allowed);

            Assert.True(result.ToCharArray().Except(allowed).Count() == 0);
        }

        [Fact]
        public async Task GenerateStringAsync_Specific()
        {
            char[] allowed = new char[] { '1', '2', '3' };
            string result = await _randomOrgClient.GetStringAsync(10, allowed);

            Assert.True(result.ToCharArray().Except(allowed).Count() == 0);
        }

        [Fact]
        public void GenerateStrings_Standard()
        {
            string[] result = _randomOrgClient.GetStrings(100, 10);

            RandomOrgOptions options = new RandomOrgOptions();
            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(options.AllowedStringCharacters).Count() == 0;
            }

            Assert.True(allowedCharacters);
        }

        [Fact]
        public async Task GenerateStringsAsync_Standard()
        {
            string[] result = await _randomOrgClient.GetStringsAsync(100, 10);

            RandomOrgOptions options = new RandomOrgOptions();
            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(options.AllowedStringCharacters).Count() == 0;
            }

            Assert.True(allowedCharacters);
        }

        [Fact]
        public void GenerateStrings_Specific()
        {
            char[] allowed = new char[] { '1', '2', '3' };
            string[] result = _randomOrgClient.GetStrings(100, 10, allowed);

            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(allowed).Count() == 0;
            }

            Assert.True(allowedCharacters);
        }

        [Fact]
        public async Task GenerateStringsAsync_Specific()
        {
            char[] allowed = new char[] { '1', '2', '3' };
            string[] result = await _randomOrgClient.GetStringsAsync(100, 10, allowed);

            bool allowedCharacters = true;
            for (int i = 0; i < result.Length; i++)
            {
                allowedCharacters &= result[i].ToCharArray().Except(allowed).Count() == 0;
            }

            Assert.True(allowedCharacters);
        }
    }
}
