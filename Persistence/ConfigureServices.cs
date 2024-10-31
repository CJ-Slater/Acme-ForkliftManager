using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TechTestConnectionString"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            services.AddSingleton<IForkliftRepository, ForkliftInMemoryRepository>();
            services.AddSingleton<IObstacleRepository, ObstacleInMemoryRepository>();
            return services;
        }

    }
}
