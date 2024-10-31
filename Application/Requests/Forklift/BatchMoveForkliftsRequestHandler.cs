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
using Application.Requests.Forklift;
using Domain.Structs;
using Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Requests.Forklifts
{
    public class BatchMoveForkliftsRequestHandler(IForkliftRepository repository, IObstacleRepository obstacleRepository) : IRequestHandler<BatchMoveForkliftRequest, BatchMoveForkliftResponse>
    {
        public async Task<BatchMoveForkliftResponse> Handle(BatchMoveForkliftRequest request, CancellationToken cancellationToken)
        {
            var forklift = await repository.GetByNameAsync(request.ForkliftName);
            var movementResults = new List<string>();
            bool encounteredObstacle = false;
            foreach (var command in request.Commands)
            {
                if (command.Distance != 0)
                {
                    var result = forklift.Move(command.Distance, obstacleRepository.GetObstacles());
                        getMovementResultString(command, result, ref movementResults);
                    if (result.encounteredObstacle)
                    {
                        encounteredObstacle = true;
                        break; //Assuming that forklift should stop processing commands if it runs into an obstacle.
                    }
                }
                else
                {
                    var result = forklift.Rotate(command.RotationDirection, command.Degrees);
                    getMovementResultString(command, result, ref movementResults);
                }
            }

            //Don't display final position information if collision occurs, as per brief
            if (!encounteredObstacle)
            {
                movementResults.Add(getFinalPositionString(forklift.Position));
                movementResults.Add(getFinalRotationString(forklift.Position));
            }

            return new BatchMoveForkliftResponse() { 
            FinalPosition = forklift.Position,
            MovementResults = movementResults
            };
        }

        //Thought about putting these into an external file, or could even make a service - however I don't know if any other part of the system would expect a string response for movement
        //or if it is only needed for this request, which is used by the UI.
        private List<string> getMovementResultString(MoveForkliftRequest command, ForkliftMovementResult result, ref List<string> movementResults)
        {
            if (command.Distance != 0)
            {
                movementResults.Add(command.Distance > 0 ? $"Move forward by {command.Distance} metres." : $"Move backward by {Math.Abs(command.Distance)} metres.");
            }
            else
            {
                movementResults.Add($"Turn {((RotationDirection)command.RotationDirection).ToString()} by {command.Degrees} degrees.");
            }

            if (result.encounteredObstacle)
                movementResults.Add($"Error: Obstacle encountered at ({result.finalPosition.X}, {result.finalPosition.Y}).");

            return movementResults;
        }

        private string getFinalPositionString(ForkliftPosition position)
        {
            return $"Final Position: ({position.X}, {position.Y})";
        }

        private string getFinalRotationString(ForkliftPosition position)
        {
            return $"Facing: {((Direction)position.Facing).ToString()}";
        }
    }
}
