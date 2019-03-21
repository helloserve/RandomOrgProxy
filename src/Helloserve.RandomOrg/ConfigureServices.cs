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

namespace Helloserve.RandomOrg
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRandomOrg(this IServiceCollection services, string apiKey)
        {
            return services.AddRandomOrg(options => { options.ApiKey = apiKey; });
        }

        public static IServiceCollection AddRandomOrg(this IServiceCollection services, Action<RandomOrgOptions> options)
        {
            services.Configure(options);
            return services.AddSingleton<IRandomOrgClient, RandomOrgClient>();
        }
    }
}
