using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Windows.Input;
using Domain.Repositories;
using Application.Dtos;

namespace Application.Requests.Forklifts
{
    public class GetAllForkliftsRequestHandler(IForkliftRepository repository) : IRequestHandler<GetAllForkliftsRequest, List<ForkliftDto>>
    {
        public async Task<List<ForkliftDto>> Handle(GetAllForkliftsRequest request, CancellationToken cancellationToken)
        {
            var forklifts =  await repository.GetAllAsync();
            var response = new List<ForkliftDto>();
            foreach (var forklift in forklifts)
            {
                response.Add(new ForkliftDto()
                {
                    ManufacturingDate = forklift.ManufacturingDate,
                    ModelNumber = forklift.ModelNumber,
                    Name = forklift.Name
                });
            }

            return response;
        }
    }
}
