using Microsoft.AspNetCore.Mvc;
using MyStudentApi.Models;
using MyStudentApi.Repository.IRepo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendanceController : ControllerBase
    {
        private readonly IAttendance _attendanceService;
        public TendanceController(IAttendance nRepo)
        {
            _attendanceService = nRepo;  
        }

        // GET: api/<TendanceController>
        [HttpGet]
        public IEnumerable<AttendanceViewModel> Get()
        {

            var box2 = _attendanceService.GetAllAttendance();
            return box2;
        }

        // GET api/<TendanceController>/5
        [HttpGet("{id}")]
        public AttendanceViewModel Get(int id)
        {
            var box= _attendanceService.GetAttendace(id);
            if (box == null) return null;

            return box;
        }

        // POST api/<TendanceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var brainBox=_attendanceService.SubmitAttendance(value);

        }


        // DELETE api/<TendanceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
