using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialMedia.Models.Database;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Database
{
    public class SocialMediaDbContext: DbContext
    {

        public DbSet<Profile> Profiles { get; set; } = null;
        public DbSet<Major> Majors { get; set; } = null;

        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);
            
            modelBuilder.Entity<StudyProgram>()
                .Property<int>("ProfileId");

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.StudyProgram)
                .WithOne()
                .HasForeignKey<StudyProgram>("ProfileId")
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Profile>()
                .Property(p => p.Roles)
                .HasConversion(
                    r => JsonConvert.SerializeObject(r),
                    r => JsonConvert.DeserializeObject<List<RoleEnum>>(r)
                );  // json serialize list of roles

            modelBuilder.Entity<Profile>()
                .Property(p => p.Race)
                .HasConversion(
                    r => RaceType.Serialize(r),
                    n => RaceType.DeSerialize(n)
                );
            
            modelBuilder.Entity<Profile>()
                .Property(p => p.Year)
                .HasConversion(
                    y => YearType.Serialize(y),
                    s => YearType.DeSerialize(s)
                );
        }
    }
}