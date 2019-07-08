using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramGeneratorWebAppContext : DbContext
    {
        public AnagramGeneratorWebAppContext (DbContextOptions<AnagramGeneratorWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AnagramGenerator.WebApp.Models.PageSearchWord> PageSearchWord { get; set; }
    }
}
