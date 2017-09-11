using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helloserve.RandomOrg
{
    public static class Configure
    {
        public static IServiceCollection AddRandomOrg(this IServiceCollection services, string apiKey)
        {
            return services.AddRandomOrg(options => { options.ApiKey = apiKey; });
        }

        public static IServiceCollection AddRandomOrg(this IServiceCollection services, Action<RandomOrgOptions> options)
        {
            RandomOrgOptions optionsObject = new RandomOrgOptions();
            options(optionsObject);
            return services.AddTransient(typeof(IRandomOrgClient), s => new RandomOrgClient(optionsObject));
        }
    }
}
