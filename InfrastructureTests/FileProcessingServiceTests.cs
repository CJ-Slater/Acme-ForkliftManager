using Microsoft.Extensions.Logging;
using System.Globalization;
using Moq;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Infrastructure.Services.File;
using Application.File;
using Infrastructure.Services.File.Mappings;

namespace InfrastructureTests
{
    [TestClass]
    public class FileProcessingServiceTests
    {
        private readonly CsvConfiguration _csvConfiguration;
       // private readonly IFileProcessingService _fileProcessingService;
        private readonly IFileValidator _fileValidator;

        public FileProcessingServiceTests()
        {
            _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

           // _fileProcessingService = new ForkliftFileProcessingService(_csvConfiguration, new FileValidator());
        }

    }
}