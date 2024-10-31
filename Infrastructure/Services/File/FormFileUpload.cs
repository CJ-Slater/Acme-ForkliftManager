using Application.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.File
{
    public class FormFileUpload : IFileUpload
    {
        private readonly IFormFile _formFile;
        //ASP implementation of generic file upload class
        public FormFileUpload(IFormFile formFile)
        {
            _formFile = formFile;
        }

        public string FileName => _formFile.FileName;

        public Stream OpenReadStream()
        {
            return _formFile.OpenReadStream();
        }
    }
}
