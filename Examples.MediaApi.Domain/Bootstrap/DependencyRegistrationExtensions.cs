using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.MediaApi.Domain
{
    public static class DependencyRegistrationExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services
                .AddHttpClient<IMediaRepository, MediaRepository>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, a => TimeSpan.FromMilliseconds(600)));
            services.AddTransient<SearchAlbumsQueryHandler>();

            return services;
        }
    }
}
