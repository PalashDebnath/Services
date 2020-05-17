using MediaServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaServices.Data
{
    public class MediaDbContext : DbContext
    {
        public MediaDbContext()
        {
        }

        public MediaDbContext(DbContextOptions<MediaDbContext> options) : base(options) { }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ShowPerson> ShowPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShowPerson>()
                        .HasKey(sc => new { sc.ShowId, sc.PersonId });
            modelBuilder.Entity<Show>()
                        .Property("Id")
                        .ValueGeneratedNever();
            modelBuilder.Entity<Person>()
                        .Property("Id")
                        .ValueGeneratedNever();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mediadb;");
        }
    }
}
