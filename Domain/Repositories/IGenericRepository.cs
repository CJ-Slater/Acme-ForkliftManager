using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
