using Domain.Enums;
using Domain.Records;
using Domain.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Forklift
    {
        public Forklift() { }
        //We need to pass something as a parameter when moving a forklift so need some form of identifier
        //Since we don't have an ID property and we are using an in memory repository for this exercise, I'm taking name as the unique identifier.
        //Alternatively could have used a new guid property
        public string Name { get; init; }
        public string ModelNumber { get; init; }
        public DateTime ManufacturingDate { get; init; }
        public ForkliftPosition Position { get; set; } = new ForkliftPosition(0, 0, 0);

        // Move - Position record can handle the logic to keep the model tidy
        public ForkliftMovementResult Move(int distance, HashSet<ObstaclePosition> obstacles)
        {
            var result = Position.Move(distance, obstacles);
            Position = result.finalPosition;
            return result;
        }

        // Rotate the forklift by multiples of 90 degrees
        public ForkliftMovementResult Rotate(RotationDirection direction, int degrees)
        {
            var result = Position.Rotate(direction, degrees);
            Position = result.finalPosition;
            return result;
        }
    }

}
