using Helloserve.RandomOrg.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Helloserve.RandomOrg.Tests
{
    public class GetUsageTests : TestClass
    {
        private IRandomOrgClient _randomOrgClient { get { return ServiceProvider.GetService<IRandomOrgClient>(); } }

        public override IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return base.ConfigureServices(services).AddRandomOrg(Constants.ApiKey);
        }

        [Fact]
        public void RandomOrg_Usage()
        {
            int remaining = _randomOrgClient.GetUsageLeft();

            Assert.True(remaining > 0);
        }
    }
}
