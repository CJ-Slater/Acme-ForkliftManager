using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IForkliftRepository : IGenericRepository<Forklift>
    {
        public Task<Forklift> GetByNameAsync(string name);
        public Task<bool> CreateAsync(Forklift user);
        public Task DeleteAllAsync();
    }
}
