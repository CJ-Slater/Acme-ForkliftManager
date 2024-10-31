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
using Domain.Records;

namespace Application.Requests.Forklifts
{
    public class ResetObstaclesRequestHandler(IObstacleRepository repository) : IRequestHandler<ResetObstaclesRequest, bool>
    {
        public Task<bool> Handle(ResetObstaclesRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(repository.ResetObstacles());
        }
    }
}
