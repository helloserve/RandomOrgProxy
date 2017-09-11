/*
Copyright 2017 Henk Roux (helloserve Productions)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
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
