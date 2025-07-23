 using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO; 
using MyStudentApi.Repository;
using MyStudentApi.Repository.IRepo;

namespace MyStudentApi.Repository
{
  
    public class LecturesRepo : ILecturesRepo
    {
        private readonly TendancyDbContext _context;
         public LecturesRepo(TendancyDbContext context)
        {
            _context = context;
         }

    

   public  async Task<List<AttendanceViewModel>> BringLecturesAttendance(int coursecode)
        {
            var attendanceViewModels = await _context.AttendanceViewModel
                            .Where(av => av.SchoolClass.CourseCode == coursecode)
                            .OrderBy(av => av.StartTime)
                            .ToListAsync();
            return attendanceViewModels;
        }

     public   async  Task<SchoolClass> CreateLectures(SchoolClass realSchoolClass)
        {
                     
              await  _context.SchoolClasses.AddAsync(realSchoolClass);
             _context.SaveChangesAsync();
            return realSchoolClass;
        }

     

     public async  Task<List<SchoolClass>> Getlectures()
        {
            var box = await _context.SchoolClasses.ToListAsync<SchoolClass>();
             return box;
        }
    }
}
