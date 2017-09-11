using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helloserve.RandomOrg.Test.Base
{
    public class TestClass
    {
        private IServiceProvider _serviceProvider;
        public IServiceProvider ServiceProvider => _serviceProvider;

        private IServiceCollection _serviceCollection;

        public TestClass()
        {
            _serviceCollection = new ServiceCollection();
            ConfigureServices(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services;
        }
    }
}
