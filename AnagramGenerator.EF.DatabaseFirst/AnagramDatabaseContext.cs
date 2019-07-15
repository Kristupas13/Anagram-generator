using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class AnagramDatabaseContext : DbContext
    {
        public AnagramDatabaseContext()
        {
        }

        public AnagramDatabaseContext(DbContextOptions<AnagramDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AnagramDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CachedWords>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnagramId).HasColumnName("AnagramID");

                entity.Property(e => e.SearchedWord)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Anagram)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CachedWords_Words");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.SearchedWord).HasMaxLength(50);

                entity.Property(e => e.UserIp)
                    .HasColumnName("UserIP")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.Property(e => e.SortedWord)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
