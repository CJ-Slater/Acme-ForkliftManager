using Domain.Enums;
using Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Structs
{
    //Went for value object approach to track forklift position. As only the data matters and not the object, value object is suited here, and pairs well with a rich doman model.
    public record ForkliftPosition(int X, int Y, Direction Facing)
    {
        //As forklift movement and positioning is a key part of the system, it makes sense to keep it as low-level as possible and go for a rich domain approach, instead of doing this elsewhere.
        //Positive distance = forwards movement. Negative = backwards.
        public ForkliftMovementResult Move(int distance, HashSet<ObstaclePosition> obstacles)
        {
            int steps = Math.Abs(distance); //Make distance positive so can use for loop.
            int stepDirection = distance > 0 ? 1 : -1; //Go forwards if distance is positive, backwards if negative.
            int targetX = X;
            int targetY = Y;

            for (int i = 0; i < steps; i++)
            {
                int currentX = targetX;
                int currentY = targetY;
                (targetX, targetY) = Facing switch
                {
                    Direction.North => (targetX, targetY + stepDirection),
                    Direction.East => (targetX + stepDirection, targetY),
                    Direction.South => (targetX, targetY - stepDirection),
                    Direction.West => (targetX - stepDirection, targetY),
                    _ => throw new InvalidOperationException("Invalid facing direction.")
                };

                if (obstacles.Contains(new ObstaclePosition(targetX, targetY)))
                {
                    //Alternatively could throw exception if obstacle is moved into, arguably it's a part of the flow though so decided on just returning it as part of the result.
                    return new ForkliftMovementResult(true, new ForkliftPosition(currentX, currentY, Facing));
                }
            }


            return new ForkliftMovementResult(false, new ForkliftPosition(targetX, targetY, Facing));
        }


        public ForkliftMovementResult Rotate(RotationDirection rotationDirection, int degrees)
        {
            // Validate degrees
            if (degrees % 90 != 0 || degrees < 0 || degrees > 360)
                throw new ArgumentException("Degrees must be a multiple of 90 and between 0 and 360.");

            // Calculate new degrees based on rotation direction
            int rotationAmount = (int)rotationDirection * degrees; // Left = -1, Right = 1
            int newDegrees = ((int)Facing + rotationAmount) % 360;

            // Ensure positive wrapping
            if (newDegrees < 0) newDegrees += 360;

            Direction newDirection = (Direction)newDegrees;
            return new ForkliftMovementResult(false, new ForkliftPosition(X, Y, newDirection));
        }

        // Ensures direction is valid within -359 to +359 degrees
        private static Direction NormalizeDirection(Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.North,
                Direction.East => Direction.East,
                Direction.South => Direction.South,
                Direction.West => Direction.West,
                _ => throw new ArgumentException("Invalid direction.")
            };
        }
    }
}
