using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Store.AutoMappers
{
    public class Config
    {
        /// <summary>
        /// get all profile and auto config
        /// </summary>
        /// <param name="services"></param>
        public static void AutoMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper();

            var profiles = typeof(ProductSkuProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
            new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });
        }
    }
}
