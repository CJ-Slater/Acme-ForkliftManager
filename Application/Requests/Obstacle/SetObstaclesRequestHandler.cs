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
    public class SetObstaclesRequestHandler(IObstacleRepository repository) : IRequestHandler<SetObstaclesRequest, bool>
    {
        public Task<bool> Handle(SetObstaclesRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(repository.SetObstacles(request.ObstaclePositions));
        }
    }
}
