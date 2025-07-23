// using Microsoft.EntityFrameworkCore;
// using MyStudentApi.Data;
// using MyStudentApi.Models;
// using System.Text.Json.Serialization;
// using System.Text.Json;
// using Microsoft.Extensions.Options;
//
// namespace MyStudentApi.Repository.IRepo
// {
//     public class AttendanceRepo : IAttendanceRepo
//     {
//         private readonly TendancyDbContext _context;
//         private readonly IEMailServices _emailServices;
//  
//          public AttendanceRepo(TendancyDbContext context, IEMailServices mailServices)
//         {
//             _context = context;
//             _emailServices = mailServices;
//               
//         }
//
//         public Task<bool> DeleteAttendanceViewModel(int CourseCode)
//         {
//             throw new NotImplementedException();
//         }
//
//         public async Task<List<AttendanceViewModel>> GetAttendanceViewModel(int courseCode)
//         {
//              var attendanceViewModels = await _context.AttendanceViewModel
//                               .Where(av => av.SchoolClass.CourseCode == courseCode)
//                               .OrderBy(av => av.StartTime)
//                               .ToListAsync();
//             return attendanceViewModels;
//         }
//
//         public async Task<IEnumerable<AttendanceViewModel>> GetAttendanceViewModels()
//         {
//               return await _context.AttendanceViewModel.ToListAsync();
//          }
//
//         public async Task<string> PostAttendanceViewModel(string studentRegNo)
//         {
//          
//
//             var student = _context.Students.FirstOrDefault(c => c.RegNo == studentRegNo);
//
//             var today = DateTime.Now;
//             var currentLecture = _context.SchoolClasses.FirstOrDefault(lecture =>
//                                                                             (lecture.DayOfWeek == today.DayOfWeek)
//                                                                              &&
//                                                                             (lecture.StartTime.TimeOfDay.Hours <= today.TimeOfDay.Hours
//                                                                              &&
//                                                                              lecture.StopTime.TimeOfDay.Hours >= today.TimeOfDay.Hours));
//
//             var tendate = new AttendanceViewModel
//             {
//                 Student = student,
//             };
//             if (currentLecture != null)
//             {
//                 var meth = (currentLecture != null) ? currentLecture : null;
//                 tendate.SchoolClass = meth;
//                 tendate.IsRegistered = true;
//                 tendate.StartTime = currentLecture.StartTime;
//                 tendate.StopTime = currentLecture.StopTime;
//                 tendate.Course = currentLecture.ClasssName;
//             }
//             else
//             {
//                  tendate.IsRegistered = false;
//             }
//             _context.AttendanceViewModel.Add(tendate);
//             await _context.SaveChangesAsync();
//
//             if (currentLecture != null)
//             {
//                 
//                 DateTime fiveMinutesBeforeEndTime = currentLecture.StopTime.AddMinutes(-5);
//                 if (true) //today >= fiveMinutesBeforeEndTime)
//                 {
//
//
//                     // Retrieve the list of students who are registered for the current lecture
//                     var studentsForClass = _context.Students.Where(s => s.SchoolClasses.Any(sc => sc.Id == currentLecture.Id))
//                                                                  .ToList();
//                     // Retrieve the list of attendance records for the current lecture
//
//                     var todaysClassAttendance = _context.AttendanceViewModel
//                               .Where(av => av.StartTime == currentLecture.StartTime && av.StopTime == currentLecture.StopTime)
//                               .Include(av => av.Student)
//                               .ToList();
//
//                     // Find the students who are registered but have no corresponding attendance record
//                     var studentsAbsentToday = studentsForClass
//                         .Where(student => !todaysClassAttendance.Any(av => av.StudentId == student.Id))
//                         .ToList();
//
//
//                     // Now you have the list of students who are registered but are absent for the current lecture
//                     foreach (var x in studentsAbsentToday)
//                     {
//
//                         Console.WriteLine($" {x.FullName} is absent for the current lecture.");
//                         _emailServices.SendEmail(x, currentLecture);
//                     }
//
//                 }
//             }
//
//             var options = new JsonSerializerOptions
//             {
//                 ReferenceHandler = ReferenceHandler.Preserve,
//                 // Other serialization options if needed
//             };
//             string jsonString =  JsonSerializer.Serialize(tendate, options);
//
//             return jsonString;
//         }
//
//
//      
//     }
// }


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Repository.IRepo;

namespace MyStudentApi.Repository
{
    public class AttendanceRepo : IAttendanceRepo
    {
        private readonly TendancyDbContext _context;

        public AttendanceRepo(TendancyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAttendanceAsync(Guid attendanceId)
        {
            var attendance = await _context.AttendanceViewModel.FindAsync(attendanceId);

            if (attendance == null)
                return false;

            _context.AttendanceViewModel.Remove(attendance);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AttendanceExistsAsync(int id)
        {
            return await _context.AttendanceViewModel.AnyAsync(e => e.Id == id);
        }

        public async Task<List<AttendanceViewModel>> GetAttendanceByCourseCode(int courseCode)
        {
            return await _context.AttendanceViewModel
                .Where(av => av.SchoolClass.CourseCode == courseCode)
                .OrderBy(av => av.StartTime)
                .ToListAsync();
        }

        public async Task<List<AttendanceViewModel>> GetAllAttendance()
        {
            return await _context.AttendanceViewModel.ToListAsync();
        }

        public async  Task<bool> PostAttendanceAsync(AttendanceViewModel attendance)
        {
            try
            {
                var box= await _context.AttendanceViewModel.AddAsync(attendance);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
             }
            return true;

        }


        public async Task<List<AttendanceViewModel>> GetTodaysAttendance(DateTime startTime, DateTime stopTime)
        {
            return _context.AttendanceViewModel
                .Where(av => av.StartTime == startTime && av.StopTime == stopTime)
                .Include(av => av.Student)
                .ToList();
        }
    }
}