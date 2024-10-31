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
    public class GetAllObstaclesRequestHandler(IObstacleRepository repository) : IRequestHandler<GetAllObstaclesRequest, HashSet<ObstaclePosition>>
    {
        public Task<HashSet<ObstaclePosition>> Handle(GetAllObstaclesRequest request, CancellationToken cancellationToken)
        {
            var obstacles =  repository.GetObstacles();
            return Task.FromResult(obstacles);
        }
    }
}
