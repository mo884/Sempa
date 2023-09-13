using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Reflection.Metadata;

namespace Sempa.Models
{
    public class mtDbContext :DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Postscs> posts { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Flight> flights { get; set; }
        public DbSet<Ticket> tickets { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DG0007O\\MSSQLSERVER02;Database=SempaTravelling;Integrated Security=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(a => a.Email).IsUnique();
            base.OnModelCreating(modelBuilder);
            
        }
        
    }
}
