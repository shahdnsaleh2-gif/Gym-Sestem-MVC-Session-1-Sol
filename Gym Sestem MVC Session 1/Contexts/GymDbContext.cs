using Microsoft.EntityFrameworkCore;
using Gym_Sestem_MVC_Session_1.Models;
using Gym_Sestem_MVC_Session_1.Configuration;

namespace Gym_Sestem_MVC_Session_1.Context
{
    public class GymDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7FAITVS\\SQL2022;Database=GymDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Plan>(new PlanConfiguration());
        }

        public DbSet<Plan> Plans { get; set; }
    }
}
