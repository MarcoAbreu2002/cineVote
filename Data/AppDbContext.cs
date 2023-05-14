using cineVote.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace cineVote.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {    
        }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Nominee> Nomines { get; set; }
        public DbSet<Category> tblCategory { get; set; }


    }
}
