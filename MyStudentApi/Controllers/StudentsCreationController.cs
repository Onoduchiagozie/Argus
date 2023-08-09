using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;
using NuGet.Packaging;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsCreationController : ControllerBase
    {
        private readonly TendancyDbContext _context;

        private readonly IMapper _mapper;

        public StudentsCreationController(TendancyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/StudentsCreation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            return await _context.Students.ToListAsync();
        }


        [HttpGet("{regNo}")]
        public async Task<ActionResult<Student>> GetStudent(string regNo) 
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            if(StudentExists(regNo))
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.RegNo == regNo);
                return student;
            }
            else
            {
                return NotFound("Reg Number Not In DataBase");
            }
 

 
        }

        [HttpPut("{regNo}")]
        public async Task<IActionResult> PutStudent(string regNo, Student student)
        {
            if (regNo != student.RegNo)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(regNo))
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

       
        [HttpPost]
        public IActionResult PostStudents(StudentsDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);
  
            // Retrieve the classes with the given classIds from the database
       

            var classesToAdd = _context.SchoolClasses.Where(c => student.CourseCodes.Contains(c.CourseCode)).ToList();
            if(classesToAdd.Count > 0)
            {
                var copyOfClassesToAdd = new List<SchoolClass>(classesToAdd);
                student.SchoolClasses.AddRange(copyOfClassesToAdd);
            }
        
            _context.Students.Add(student);          

            _context.SaveChanges();
            return Ok();
        }


        // DELETE BY ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(string regNo)
        {
            return (_context.Students?.Any(e => e.RegNo == regNo)).GetValueOrDefault();
        }
    }
}
