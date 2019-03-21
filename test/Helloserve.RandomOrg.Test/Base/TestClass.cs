/*
Copyright 2019 Henk Roux (helloserve Productions)

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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Helloserve.RandomOrg.Test.Base
{
    public class TestClass
    {
        public IServiceProvider ServiceProvider { get; }

        private IServiceCollection _serviceCollection;

        public TestClass()
        {
            _serviceCollection = new ServiceCollection();
            ConfigureServices(_serviceCollection);
            ServiceProvider = _serviceCollection.BuildServiceProvider();
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services;
        }
    }
}
