using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.File
{
    public class FileValidator : IFileValidator
    {
        public bool ValidateFileFormat(IFileUpload fileInput, string[] allowedExtensions)
        {
            //Additionally you can get the mime type from the content to ensure that the file extension isn't being spoofed - I feel that's out of scope of the brief though
            var extension = Path.GetExtension(fileInput.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }

        public bool ValidateFileSize(IFileUpload fileInput, long maxSizeBytes)
        {
            return fileInput.OpenReadStream().Length <= maxSizeBytes;
        }
    }



}