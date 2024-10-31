using Application.Requests.Forklifts;
using Application.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Requests.Forklift;
using Domain.Structs;

namespace Infrastructure.Services.ForkliftServices
{
    public interface IForkliftMovementCommandService<TRequest, TResponse> where TRequest: class
    {
        //Generic interface that converts a request of any type into a list of movement commands that can be picked up by the system
        public Task<TResponse> ParseMovementAsync(TRequest movementCommand, string forkliftName);
    }
}
