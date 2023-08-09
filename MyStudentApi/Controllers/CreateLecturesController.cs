using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateLecturesController : ControllerBase
    {
        private readonly TendancyDbContext _context;
        private readonly IMapper _mapper;

        public CreateLecturesController(TendancyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Lectureres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolClass>>> GetSchoolClasses()
        {
          if (_context.SchoolClasses == null)
          {
              return NotFound();
          }
            return await _context.SchoolClasses.ToListAsync();
        }

        // GET: api/Lectureres/5
        [HttpGet("{courseCode}")]
        public async Task<ActionResult<SchoolClass>> GetSchoolClass(int courseCode)
        {
          if (_context.SchoolClasses == null)
          {
              return NotFound();
          }
             if(SchoolClassExists(courseCode))
                {
                  var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(x => x.CourseCode == courseCode);
                  return schoolClass;
            }
            else
            {
                return NotFound("Course Does Not Exist In Our Database");
            }
        }
         
        [HttpPut("{coursecode}")]
        public async Task<IActionResult> PutSchoolClass(int coursecode, SchoolClass schoolClass)
        {
            if (coursecode != schoolClass.CourseCode)
            {
                return BadRequest();
            }

            _context.Entry(schoolClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassExists(coursecode))
                {
                    return NotFound("Error at Put Request, Verify CourseCode Exists");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
         
        
        [HttpPost]
        public async Task<ActionResult<SchoolClassDTO>> PostSchoolClass(SchoolClassDTO schoolClass)
        {
          if (_context.SchoolClasses == null)
                return Problem("Entity set 'SchoolClasses' is null.");
            var realSchoolClass=_mapper.Map<SchoolClass>(schoolClass);
            
            _context.SchoolClasses.Add(realSchoolClass);
            await _context.SaveChangesAsync();

            return Ok(realSchoolClass);
        }

        
        [HttpDelete("{CourseCode}")]
        public async Task<IActionResult> DeleteSchoolClass(int CourseCode)
        {
            if (_context.SchoolClasses == null)
            {
                return NotFound();
            }
            if (SchoolClassExists(CourseCode))
            {
                var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(x => x.CourseCode == CourseCode);
                _context.SchoolClasses.Remove(schoolClass);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound("Request Couse Does Not ExIST Check YAh CourseCode");
            }
        


        }

        private bool SchoolClassExists(int CourseCode)
        {
            return (_context.SchoolClasses?.Any(e => e.CourseCode == CourseCode)).GetValueOrDefault();
        }
    }
}
