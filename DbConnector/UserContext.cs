using MovieBackend.DTOs;
using MovieBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieBackend.DbConnector
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cart> Carts { get; set; }



        //Had an error with the attribute "LifetimeGrossInUSD" when building the database. Which is fixed with the below code.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                // Other property configurations...

                entity.Property(e => e.LifetimeGrossInUSD)
                      .HasColumnType("decimal(18, 2)"); // Adjust precision

            });



        }
        }
}
