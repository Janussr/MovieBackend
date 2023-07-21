using CodeFirstProject.DTOs;
using CodeFirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstProject.DbConnector
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
