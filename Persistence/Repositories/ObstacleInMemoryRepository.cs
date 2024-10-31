using Domain.Records;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ObstacleInMemoryRepository : IObstacleRepository
    {

        private readonly HashSet<ObstaclePosition> defaultObstacles;
        private HashSet<ObstaclePosition> currentObstacles;

        public ObstacleInMemoryRepository()
        {
            defaultObstacles = new HashSet<ObstaclePosition>
            {
                new ObstaclePosition(2, 3),
                new ObstaclePosition(4, 5)
            };

            // Initialize current obstacles with defaults
            ResetObstacles();
        }

        public HashSet<ObstaclePosition> GetObstacles()
        {
            return new HashSet<ObstaclePosition>(currentObstacles);
        }

        public bool ResetObstacles()
        {
            currentObstacles = new HashSet<ObstaclePosition>(defaultObstacles);
            return true;
        }

        public bool SetObstacles(HashSet<ObstaclePosition> obstacles)
        {
            currentObstacles = obstacles;
            return true;
        }
    }
}
