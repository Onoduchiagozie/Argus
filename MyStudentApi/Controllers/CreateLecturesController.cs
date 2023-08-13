using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;
using MyStudentApi.Repository.IRepo;

namespace MyStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateLecturesController : ControllerBase
    {
        private readonly TendancyDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILecturesRepo _lecturesRepo;

        public CreateLecturesController(TendancyDbContext context,IMapper mapper, ILecturesRepo lecturesRepo)
        {
            _context = context;
            _mapper = mapper;
            _lecturesRepo = lecturesRepo;

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

        // GET: Class With Kids That Registered For It
        [HttpGet("{courseCode}")]
        public async Task<ActionResult<SchoolClass>> GetSchoolClass(int courseCode)
        { 
         
             if(SchoolClassExists(courseCode))
                {
                 var schoolClass = await _context.SchoolClasses.Include(c=>c.Students).FirstOrDefaultAsync(x => x.CourseCode == courseCode);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                var json = JsonSerializer.Serialize(schoolClass, options);
                return Content(json, "application/json");
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
            var realSchoolClass = _mapper.Map<SchoolClass>(schoolClass);
            var box = await _lecturesRepo.CreateLectures(realSchoolClass);
            return Ok(box);
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
