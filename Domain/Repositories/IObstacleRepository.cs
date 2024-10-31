using Domain.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    //Decided not to use generic repo for obstacle since it doesn't really represent an actual domain entity
    public interface IObstacleRepository
    {
        HashSet<ObstaclePosition> GetObstacles();
        bool SetObstacles(HashSet<ObstaclePosition> obstacle);
        bool ResetObstacles();
    }
}
