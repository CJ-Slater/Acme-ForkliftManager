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
    public record ForkliftMovementResult(bool encounteredObstacle, ForkliftPosition finalPosition)
    {
    }
}
