using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.File
{
    public interface IFileValidator
    {
        bool ValidateFileFormat(IFileUpload fileInput, string[] allowedExtensions);
        bool ValidateFileSize(IFileUpload fileInput, long maxSizeBytes);
    }
}
