using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.File
{
    public interface IFileUpload
    {
        //This allows the inner layers to be independent of what technology we use for web api e.g. Allows us to not have to be dependent on ASP .NET to use IFormFile
        string FileName { get; }
        Stream OpenReadStream();
    }
}
