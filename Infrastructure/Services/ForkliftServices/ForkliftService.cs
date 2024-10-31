using Application.Requests.Forklifts;
using Application.File;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Infrastructure.Services.File;
using Infrastructure.Services.File.Mappings;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Infrastructure.Services.ForkliftServices
{
    public class ForkliftService : IForkliftService
    {
        private readonly ISender _mediator;
        private readonly IFileProcessingService<Forklift> _fileprocessingService;

        public ForkliftService(ISender mediator, IFileProcessingService<Forklift> fileprocessingService)
        {
            _mediator = mediator;
            _fileprocessingService = fileprocessingService;
        }
        public async Task<CreateBatchForkliftRequestResponse> UploadForkliftFile(IFileUpload file)
        {

            var request = new CreateBatchForkliftRequest();
                request.Forklifts = await _fileprocessingService.ProcessFileAsync(file);
            return await _mediator.Send(request);
        }
    }
}
