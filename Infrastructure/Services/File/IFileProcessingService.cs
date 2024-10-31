using Application.File;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.File
{
    public interface IFileProcessingService<T>
    {
        Task<List<T>> ProcessFileAsync(IFileUpload file);
    }
}
