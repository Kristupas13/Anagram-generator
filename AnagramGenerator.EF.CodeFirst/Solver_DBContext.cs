using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnagramGenerator.EF.CodeFirst
{
    public class Solver_DBContext : DbContext
    {
        public DbSet<WordEntity> Words { get; set; }
        public DbSet<CachedEntity> CachedWords { get; set; }
        public DbSet<UserLogEntity> UserLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB.;Initial Catalog=Solver_DB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
