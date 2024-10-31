using Application.Requests.Forklift;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Services.File;
using Infrastructure.Services.ForkliftServices;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFileProcessingService<Forklift>, ForkliftFileProcessingService>();
            services.AddTransient<IForkliftService, ForkliftService>();
            services.AddTransient<IForkliftMovementCommandService<string, BatchMoveForkliftResponse>, ForkliftStringCommandService>();
            //If we get csv files with different formats I wouldn't do this since this will apply globally.
            services.AddSingleton(new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower() //CSVHelper expects camel case by default, not pascal case. This ignores case on the header.
            });
            services.AddSingleton(new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower() //CSVHelper expects camel case by default, not pascal case. This ignores case on the header.
            });
            return services;
        }

    }
}
