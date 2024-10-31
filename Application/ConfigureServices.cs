using Application.File;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }
            services.AddTransient<IFileValidator, FileValidator>();
            return services;
        }

    }
}
