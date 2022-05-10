using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Participant> Participants { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Departments Table
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "IT" });
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 2, CourseName = "HR" });
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 3, CourseName = "Payroll" });
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 4, CourseName = "Admin" });

            // Seed Employee Table
            modelBuilder.Entity<Participant>().HasData(new Participant
            {
                ParticipantId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                CourseId = 1,
                PhotoPath = "images/john.png"
            });

            modelBuilder.Entity<Participant>().HasData(new Participant
            {
                ParticipantId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                CourseId = 2,
                PhotoPath = "images/sam.jpg"
            });

            modelBuilder.Entity<Participant>().HasData(new Participant
            {
                ParticipantId = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                CourseId = 1,
                PhotoPath = "images/mary.png"
            });

            modelBuilder.Entity<Participant>().HasData(new Participant
            {
                ParticipantId = 4,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                CourseId = 3,
                PhotoPath = "images/sara.png"
            });
        }
    }
}
