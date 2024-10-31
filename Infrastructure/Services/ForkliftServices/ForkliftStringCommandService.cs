using Application.Requests.Forklift;
using Domain.Structs;
using MediatR;
using System.Text.RegularExpressions;

namespace Infrastructure.Services.ForkliftServices
{
    public class ForkliftStringCommandService : IForkliftMovementCommandService<string, BatchMoveForkliftResponse>
    {
        private readonly ISender _mediator;

        public ForkliftStringCommandService(ISender mediator)
        {
            _mediator = mediator;
        }
        public async Task<BatchMoveForkliftResponse> ParseMovementAsync(string commandString, string forkliftName)
        {
            var commandStringsSplit = SplitCommandString(commandString);
            var commandList = new List<MoveForkliftRequest>();
            foreach (var command in commandStringsSplit)
            {
                commandList.Add(ProcessMovementCommand(command));
            }

            var moveRequest = new BatchMoveForkliftRequest()
            {
                ForkliftName = forkliftName,
                Commands = commandList
            };

            return await _mediator.Send(moveRequest);
        }

        private List<string> SplitCommandString(string commandString)
        {
            var commandList = Regex.Split(commandString, @"(?=[FBLR])"); //Split the command string on any occurence of F, B, L or R.
            return commandList.Where(c => !string.IsNullOrEmpty(c)).ToList(); //Have to drop the first entry as the first entry will always be an empty string. There's a probably a way to ignore just the first hit using Regex, but I wasn't able to crack it.
        }

        private MoveForkliftRequest ProcessMovementCommand(string movementCommand)
        {
            var movementRequest = new MoveForkliftRequest();
            int distanceMultiplier = 0;

            switch (movementCommand[0])
            {
                case 'F':
                    distanceMultiplier = 1;
                    break;
                case 'B':
                    distanceMultiplier = -1;
                    break;
                case 'L':
                    movementRequest.RotationDirection = Domain.Enums.RotationDirection.Left;
                    break;
                case 'R':
                    movementRequest.RotationDirection = Domain.Enums.RotationDirection.Right;
                    break;
                default: throw new InvalidDataException("The movement command given is not valid - each command must start with either an F, B, L or R.");

            }
            int amount = 0;
            try { 
                //Intentionally not using tryparse here as I want to throw an exception if the command isn't correctly formatted.
                amount = Int32.Parse(movementCommand.Substring(1));
            }
            catch (Exception ex){
                throw new InvalidDataException("The movement command given is not valid - each command must end with a positive number", ex);
            }

            if (amount < 0)
                throw new InvalidDataException("The movement command given is not valid - each command must end with a positive number");

            if (distanceMultiplier != 0)
            {
                movementRequest.Distance = amount * distanceMultiplier;
            }
            else if (movementRequest.RotationDirection != 0)
            {
                movementRequest.Degrees = amount;
            }

            return movementRequest;
        }
    }
}