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
    public class ForkliftSqlRepository : IForkliftRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ForkliftSqlRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Forklift Forklift)
        {
            await _dbContext.Forklifts.AddAsync(Forklift);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //Used just to reset the table so you can upload a different set if needed.
        public async Task DeleteAllAsync()
        {
            await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Forklifts]");
        }
        public async Task<IEnumerable<Forklift>> GetAllAsync()
        {
            return await _dbContext.Forklifts.ToListAsync();
        }

        public async Task<Forklift> GetByNameAsync(string name)
        {
            return (await GetAsync(c => c.Name == name)).Single();
        }

        public async Task<IEnumerable<Forklift>> GetAsync(Expression<Func<Forklift, bool>> filter = null)
        {
            IQueryable<Forklift> query = _dbContext.Forklifts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

    }
}
