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
        public async Task<ActionResult<IEnumerable<Student>>> GetAttendanceViewModel()
        {
            if (_context.AttendanceViewModel == null)
            {
                return NotFound();
            }
            var today = DateTime.Now;
            var currentLecture = _context.SchoolClasses.FirstOrDefault(lecture =>
                                                                            (lecture.DayOfWeek == today.DayOfWeek)
                                                                             &&
                                                                            (lecture.StartTime.TimeOfDay <= today.TimeOfDay
                                                                             &&
                                                                             lecture.StopTime.TimeOfDay >= today.TimeOfDay));
            var studentsForClass = _context.Students
                                         .Where(s =>
                                         s.AttendanceViewModel
                                         .Any(cs => cs.SchoolClass.ClasssName == currentLecture.ClasssName))
                                         .ToList();
            var todaysClassAttendance = _context.AttendanceViewModel.Where(
                                             av => av.StartTime == currentLecture.StartTime && av.StopTime == currentLecture.StopTime)
                                                 .Include(av => av.Student)
                                                 .Include(av => av.SchoolClass)
                                                 .ToList();


            var mystudentsNotPresentToday = studentsForClass.Except(todaysClassAttendance.Select(av => av.Student)).ToList();

            var studentsNotPresentToday = studentsForClass.Where(student => !todaysClassAttendance.Any(av => av.StudentId == student.Id)).ToList();
            return  mystudentsNotPresentToday;
        }

        // GET: api/Attendance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceViewModel>> GetAttendanceViewModel(int id)
        {
            if (_context.AttendanceViewModel == null)
            {
                return NotFound();
            }
            var attendanceViewModel = await _context.AttendanceViewModel.FindAsync(id);

            if (attendanceViewModel == null)
            {
                return NotFound();
            }

            return attendanceViewModel;
        }

        // PUT: api/Attendance/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceViewModel(int id, AttendanceViewModel attendanceViewModel)
        {
            if (id != attendanceViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendanceViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attendance
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
                        Console.WriteLine($" Email for {x.FullName} Sent .");
                    }

                }
                else
                {
                     
 
                     // There is no ongoing class at the current time, you can handle this scenario accordingly
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendanceViewModel(int id)
        {
            if (_context.AttendanceViewModel == null)
            {
                return NotFound();
            }
            var attendanceViewModel = await _context.AttendanceViewModel.FindAsync(id);
            if (attendanceViewModel == null)
            {
                return NotFound();
            }

            _context.AttendanceViewModel.Remove(attendanceViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceViewModelExists(int id)
        {
            return (_context.AttendanceViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
