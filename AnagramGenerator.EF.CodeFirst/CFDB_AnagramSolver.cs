using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnagramGenerator.EF.CodeFirst
{
    public class CFDB_AnagramSolverContext : DbContext
    {
        public DbSet<WordEntity> Words { get; set; }
        public DbSet<CachedEntity> CachedWords { get; set; }
        public DbSet<UserLogEntity> UserLogs { get; set; }
        public DbSet<RequestEntity> RequestWords { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB.;Initial Catalog=CFDB_AnagramSolver;Integrated Security=True");
                base.OnConfiguring(optionsBuilder);
            }

        }
        public CFDB_AnagramSolverContext(DbContextOptions<CFDB_AnagramSolverContext> options)
           : base(options)
        { }
    }
}
