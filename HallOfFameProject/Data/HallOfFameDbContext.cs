using Audit.EntityFramework;
using HallOfFameProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HallOfFameProject.Data
{
    public class HallOfFameDbContext : AuditDbContext
    {
        public HallOfFameDbContext()
        {
        }

        public HallOfFameDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<AuditSkill> AuditSkill { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Skill>()
        //        .HasOne(a => a.Person)
        //        .WithMany(b => b.Skills)
        //        .OnDelete(DeleteBehavior.Cascade);
        //
        //  Default SwaggerUI doesn't work properly this way
        //}        


    }
}

