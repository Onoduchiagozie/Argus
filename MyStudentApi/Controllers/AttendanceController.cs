using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly TendancyDbContext _context;

        public AttendanceController(TendancyDbContext context)
        {
            _context = context;
        }

        // GET: api/Attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceViewModel>>> GetAttendanceViewModel()
        {
            if (_context.AttendanceViewModel == null)
            {
                return NotFound();
            }
            return await _context.AttendanceViewModel.ToListAsync();
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
                                                                            (lecture.StartTime.TimeOfDay <= today.TimeOfDay
                                                                             &&
                                                                             lecture.StopTime.TimeOfDay >= today.TimeOfDay));


            var tendate = new AttendanceViewModel
            {
                Student = student,  
                
            };
            if (currentLecture != null)
            {
                var meth= (currentLecture != null) ? currentLecture : null;
                tendate.SchoolClass = meth;
               
                tendate.IsRegistered = true;
            }
            else
            {
                tendate.SchoolClassId= null;
                tendate.IsRegistered = false;
            }
 
            _context.AttendanceViewModel.Add(tendate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendanceViewModel", new { id = tendate.Id }, tendate);
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
