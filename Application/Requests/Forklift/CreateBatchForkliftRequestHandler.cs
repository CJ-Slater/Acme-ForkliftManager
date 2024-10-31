using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Windows.Input;
using Domain.Repositories;

namespace Application.Requests.Forklifts
{
    public class CreateBatchForkliftRequestHandler(IForkliftRepository repository) : IRequestHandler<CreateBatchForkliftRequest, CreateBatchForkliftRequestResponse>
    {
        public async Task<CreateBatchForkliftRequestResponse> Handle(CreateBatchForkliftRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateBatchForkliftRequestResponse()
            {
                SuccessfulInsertsCount = 0,
                FailedInsertsCount = 0
            };
            if (request.Forklifts.Count == 0)
            {
                return response;
            }


            foreach (var forklift in request.Forklifts)
            {
                await repository.CreateAsync(forklift);
                response.SuccessfulInsertsCount++;
            }


            response.FailedInsertsCount = request.Forklifts.Count - response.SuccessfulInsertsCount;

            return response;
        }

    }
}
