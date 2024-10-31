using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private IDbContextOptions Options {  get; init; }
        public readonly IConfiguration Configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            Options = options;
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Forklift> Forklifts { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
