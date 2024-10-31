using Application.File;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Services.File.Mappings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.File
{
    public class ForkliftFileProcessingService : IFileProcessingService<Forklift>
    {
        private readonly CsvConfiguration _config;
        private readonly IFileValidator _fileValidator;

        public ForkliftFileProcessingService(CsvConfiguration config, IFileValidator fileValidator)
        {
            _config = config;
            _fileValidator = fileValidator;
        }

        public async Task<List<Forklift>> ProcessFileAsync(IFileUpload fileInput)
        {
            if (fileInput == null || fileInput.OpenReadStream().Length == 0)
            {
                throw new ArgumentException("File is empty or null.");
            }
            // Validate format is correct
            var formatValidationResult = _fileValidator.ValidateFileFormat(fileInput, new string[] { ".csv", ".json" });
            if (!formatValidationResult)
            {
                throw new InvalidOperationException("Not a valid CSV or json file."); ;
            }


            // Step 3: Parse content
            using var stream = new StreamReader(fileInput.OpenReadStream());
            List<Forklift> forklifts = Path.GetExtension(fileInput.FileName).ToLower() switch
            {
                ".csv" => ParseCsv(stream),
                ".json" => await ParseJsonAsync(stream),
                _ => throw new NotSupportedException("File type not supported.")
            };

            return forklifts;
        }


        //Generic parse csv method that will attempt to turn a csv file into a class.
        public List<Forklift> ParseCsv(StreamReader stream)
        {

            List<Forklift> records = new List<Forklift>();

            try
            {
                using (var csv = new CsvReader(stream, _config))
                {
                    csv.Context.RegisterClassMap<ForkliftMap>();
                    records = csv.GetRecords<Forklift>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while processing the CSV file.", ex);
            }

            return records;
        }

        private async Task<List<Forklift>> ParseJsonAsync(StreamReader stream)
        {
            var content = await stream.ReadToEndAsync();

            try
            {
                // Try to parse the JSON; if this fails, it’s not valid JSON
                return JsonConvert.DeserializeObject<List<Forklift>>(content);
            }
            catch (JsonException)
            {
                throw new FormatException("File does not appear to be valid JSON.");
            }
        }
    }
}
