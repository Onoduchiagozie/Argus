using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyStudentApi.Models;
using System.Diagnostics;

namespace MyStudentApi.Data
{
    public class TendancyDbContext : DbContext
    {

        public TendancyDbContext(DbContextOptions<TendancyDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<AttendanceViewModel> AttendanceViewModel { get; set; }
        public DbSet<StudentSchoolClass> StudentSchoolClass { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<Student>()
         .HasMany(s => s.SchoolClasses)
         .WithMany(c => c.Students)
         .UsingEntity<StudentSchoolClass>(
             j => j.HasOne(ssc => ssc.SchoolClass).WithMany(),
             j => j.HasOne(ssc => ssc.Student).WithMany(),
             j => j.ToTable("StudentSchoolClass")
         );
        }
    }
}
