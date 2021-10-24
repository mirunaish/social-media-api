using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialMedia.Models.Database;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Database
{
    public class SocialMediaDbContext: DbContext
    {

        public DbSet<Profile> Profiles { get; set; } = null;  // for accessing the profiles table in the database
        public DbSet<Major> Majors { get; set; } = null;  // majors table
        // don't need to directly access StudyProgram table

        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<StudyProgram>()
                .Property<int>("ProfileId");
            // create a property ProfileId 

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.StudyProgram)
                .WithOne()  // one-to-one relationship
                .HasForeignKey<StudyProgram>("ProfileId")  // make this property a foreign key
                .OnDelete(DeleteBehavior.Cascade);  // if a profile is deleted so is its studyProgram
            
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
                );  // serialize race as an int
            
            modelBuilder.Entity<Profile>()
                .Property(p => p.Year)
                .HasConversion(
                    y => YearType.Serialize(y),
                    s => YearType.DeSerialize(s)
                );  // serialize year as a string
        }
    }
}