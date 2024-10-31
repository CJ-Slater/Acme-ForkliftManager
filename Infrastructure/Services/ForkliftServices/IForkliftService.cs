using Application.Requests.Forklifts;
using Application.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.ForkliftServices
{
    public interface IForkliftService
    {
        public Task<CreateBatchForkliftRequestResponse> UploadForkliftFile(IFileUpload file);
    }
}
