using AnagramGenerator.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnagramGenerator.EF.CodeFirst
{
    public class CFDB_Context : DbContext
    {
        public DbSet<WordEntity> Words { get; set; }
        public DbSet<CachedEntity> CachedWords { get; set; }
        public DbSet<UserLogEntity> UserLogs { get; set; }
        public DbSet<ModificationEntity> Modifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\MSSQLLocalDB.;Initial Catalog=CFDB;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
