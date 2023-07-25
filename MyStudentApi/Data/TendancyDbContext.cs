using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttendanceViewModel>()
                .Property(s => s.SchoolClassId)
                .IsRequired(false); // This makes the SchoolClassId nullable in the database
        }


        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Student>()
               .HasOne(s => s.SchoolClass)
               .WithMany(sc => sc.Students)
               .HasForeignKey(s => s.SchoolClassId)
               .OnDelete(DeleteBehavior.NoAction);
         }*/
    }
}
