using Cursova.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cursova
{
    internal class SchoolDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Skip> Skips { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
