﻿// <auto-generated />
using System;
using AnagramGenerator.EF.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramGenerator.EF.CodeFirst.Migrations
{
    [DbContext(typeof(CFDB_AnagramSolverContext))]
    partial class CFDB_AnagramSolverContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.CachedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnagramId");

                    b.Property<int>("RequestId");

                    b.HasKey("Id");

                    b.HasIndex("AnagramId");

                    b.HasIndex("RequestId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.RequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("RequestWords");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Counter");

                    b.Property<string>("Ip");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.UserLogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date");

                    b.Property<int>("RequestId");

                    b.Property<int>("UserId");

                    b.Property<string>("UserIp");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.WordEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SortedWord");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.CachedEntity", b =>
                {
                    b.HasOne("AnagramGenerator.EF.CodeFirst.Models.WordEntity", "Anagram")
                        .WithMany("CachedEntity")
                        .HasForeignKey("AnagramId");

                    b.HasOne("AnagramGenerator.EF.CodeFirst.Models.RequestEntity", "Request")
                        .WithMany("CachedEntity")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnagramGenerator.EF.CodeFirst.Models.UserLogEntity", b =>
                {
                    b.HasOne("AnagramGenerator.EF.CodeFirst.Models.RequestEntity", "Request")
                        .WithMany("UserLogEntity")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnagramGenerator.EF.CodeFirst.Models.UserEntity", "User")
                        .WithMany("UserLogEntity")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}