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
    }
}
