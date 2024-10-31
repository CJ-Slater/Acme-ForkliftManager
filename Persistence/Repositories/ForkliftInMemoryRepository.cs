using Domain.Entities;
using Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ForkliftInMemoryRepository : IForkliftRepository
    {
        private readonly List<Forklift> _forklifts;

        public ForkliftInMemoryRepository()
        {
                _forklifts = new List<Forklift>();
        }
        public ForkliftInMemoryRepository(List<Forklift> forklifts)
        {
            if (forklifts == null)
                forklifts = new List<Forklift>();

            _forklifts = forklifts;
        }

        public async Task<bool> CreateAsync(Forklift Forklift)
        {
            //Ensure that name is unique since I am using it as a unique identifier.
            if (_forklifts.Any(c => c.Name.Equals(Forklift.Name)))
            {
                throw new InvalidOperationException($"A forklift with the name '{Forklift.Name}' already exists.");
            }
            _forklifts.Add(Forklift);
            return true;
        }

        //Used just to reset the table so you can upload a different set if needed.
        public async Task DeleteAllAsync()
        {
            _forklifts?.Clear();
        }
        public async Task<IEnumerable<Forklift>> GetAllAsync()
        {
            return _forklifts;
        }

        public async Task<Forklift> GetByNameAsync(string name)
        {
            
            return (await GetAsync(c => c.Name == name)).Single();
        }

        public async Task<IEnumerable<Forklift>> GetAsync(Expression<Func<Forklift, bool>> filter = null)
        {
           var results = new List<Forklift>();

            if (filter != null)
            {
                results = _forklifts.AsQueryable().Where(filter).ToList();
            }
            else
            {
                results = _forklifts;
            }

            return results;
        }

    }
}
