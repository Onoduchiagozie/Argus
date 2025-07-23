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
      //  private readonly TendancyDbContext _context;
        private IAttendanceRepo _attendanceRepo;
        private readonly IEMailServices _mailService;
 
        public AttendanceController( IEMailServices mailService, IAttendanceRepo attendanceRepo)
        {
        //    _context = context;
            _mailService = mailService;
            _attendanceRepo = attendanceRepo;
         }

        // GET: api/Attendance
        [HttpGet]
        public async Task<List<AttendanceViewModel>> GetAttendanceViewModel()
        {
            var box = await _attendanceRepo.GetAllAttendance();
                 return box;
        }

        // GET: api/Attendance/5
        [HttpGet("{courseCode}")]
        public async Task<ActionResult<AttendanceViewModel>> GetAttendanceViewModel(int courseCode)
        {

             var attendanceViewModel = _attendanceRepo.GetAttendanceByCourseCode(courseCode);
             return Ok(attendanceViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostAttendanceViewModel(string studentRegNo)
        {
            var newVm=new AttendanceViewModel();
            var box =await _attendanceRepo.PostAttendanceAsync(newVm);  
            return Ok(box);
        }

        // DELETE: api/Attendance/5
        [HttpDelete("{CourseCode}")]
        public async Task<IActionResult> DeleteAttendanceViewModel(Guid CourseCode)
        {


           var box = await _attendanceRepo.DeleteAttendanceAsync(CourseCode);

            return NoContent();
        }

    
    }
}
