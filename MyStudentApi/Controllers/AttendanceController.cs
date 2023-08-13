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
        private IAttendanceRepo _attendanceRepo;
        private readonly IEMailServices _mailService;
 
        public AttendanceController(TendancyDbContext context, IEMailServices mailService, IAttendanceRepo attendanceRepo)
        {
            _context = context;
            _mailService = mailService;
            _attendanceRepo = attendanceRepo;
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
        public async Task<ActionResult<AttendanceViewModel>> GetAttendanceViewModel(int courseCode)
        {

            //      var attendanceViewModel =await _attendanceRepo.GetAttendanceViewModel(courseCode);
            var attendanceViewModel = _context.AttendanceViewModel.Where(x => x.SchoolClass.CourseCode == courseCode);

         


            return Ok(attendanceViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAttendanceViewModel(string studentRegNo)
        {
            var box =await _attendanceRepo.PostAttendanceViewModel(studentRegNo);  
            return Ok(box);
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
