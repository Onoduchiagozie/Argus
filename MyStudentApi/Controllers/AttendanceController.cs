using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MailKit;
using MyStudentApi.Repository.IRepo;

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly TendancyDbContext _context;
        private readonly IEMailServices _mailService;

        public AttendanceController(TendancyDbContext context, IEMailServices mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        // GET: api/Attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceViewModel>>> GetAttendanceViewModel()
        {
            var box= await _context.AttendanceViewModel.ToListAsync();
            return box;
        }

        // GET: api/Attendance/5
        [HttpGet("{courseCode}")]
        public async Task<IList<AttendanceViewModel>> GetAttendanceViewModel(int courseCode)
        {
        
            if(courseCode != null)
            {
                var attendanceViewModel = _context.AttendanceViewModel.Where(x => x.SchoolClass.CourseCode == courseCode).ToListAsync();
                return (IList<AttendanceViewModel>)attendanceViewModel;
            }
            else
            {
                return (IList<AttendanceViewModel>)NotFound();
            }
                      
        }
         
        [HttpPost]
        public async Task<ActionResult<int>> PostAttendanceViewModel(string studentRegNo)
        {
            if (_context.AttendanceViewModel == null)
            {
                return Problem("Entity set 'TendancyDbContext.AttendanceViewModel'  is null.");
            }

            var student = _context.Students.FirstOrDefault(c => c.RegNo == studentRegNo);

            var today = DateTime.Now;
            var currentLecture = _context.SchoolClasses.FirstOrDefault(lecture =>
                                                                            (lecture.DayOfWeek == today.DayOfWeek) 
                                                                             &&
                                                                            (lecture.StartTime.TimeOfDay.Hours <= today.TimeOfDay.Hours
                                                                             &&
                                                                             lecture.StopTime.TimeOfDay.Hours >= today.TimeOfDay.Hours));

            var tendate = new AttendanceViewModel
            {
                Student = student,                  
            };
            if (currentLecture != null)
            {
                var meth= (currentLecture != null) ? currentLecture : null;
                tendate.SchoolClass = meth;
                tendate.IsRegistered = true;
                tendate.StartTime = currentLecture.StartTime;
                tendate.StopTime = currentLecture.StopTime;
                tendate.Course = currentLecture.ClasssName;
            }
            else
            {
                tendate.SchoolClassId= null;
                tendate.IsRegistered = false;
            }
            _context.AttendanceViewModel.Add(tendate);
            await _context.SaveChangesAsync();

            if (currentLecture != null)
            {
                DateTime fiveMinutesBeforeEndTime = currentLecture.StopTime.AddMinutes(-5);

                if (true) //today >= fiveMinutesBeforeEndTime)
                {
 

                    // Retrieve the list of students who are registered for the current lecture
                    var studentsForClass = _context.Students.Where(s => s.SchoolClasses.Any(sc => sc.Id == currentLecture.Id))
                                                                 .ToList();
                    // Retrieve the list of attendance records for the current lecture

                    var todaysClassAttendance = _context.AttendanceViewModel
                              .Where(av => av.StartTime == currentLecture.StartTime && av.StopTime == currentLecture.StopTime)
                              .Include(av => av.Student)
                              .ToList();

                    // Find the students who are registered but have no corresponding attendance record
                    var studentsAbsentToday = studentsForClass
                        .Where(student => !todaysClassAttendance.Any(av => av.StudentId == student.Id))
                        .ToList();


                    // Now you have the list of students who are registered but are absent for the current lecture
                    foreach (var x in studentsAbsentToday)
                    {
             
                        Console.WriteLine($" {x.FullName} is absent for the current lecture.");
                        _mailService.SendEmail(x,currentLecture);
                    }

                }
         


            }
            // Check if the current lecture exists
 

          




            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Other serialization options if needed
            };

            string jsonString = JsonSerializer.Serialize(tendate, options);

            return Ok(jsonString);
        }

        // DELETE: api/Attendance/5
        [HttpDelete("{CourseCode}")]
        public async Task<IActionResult> DeleteAttendanceViewModel(int CourseCode)
        {
            if (_context.AttendanceViewModel == null)
            {
                return NotFound();
            }
            var attendanceViewModel = _context.AttendanceViewModel.Where(x => x.SchoolClass.CourseCode == CourseCode);
            if (attendanceViewModel == null)
            {
                return NotFound("Issue with getting Attendance Range Or Something");
            }
            _context.AttendanceViewModel.RemoveRange(attendanceViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceViewModelExists(int id)
        {
            return (_context.AttendanceViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
