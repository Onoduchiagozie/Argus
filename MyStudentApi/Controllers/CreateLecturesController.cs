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
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolClass>> GetSchoolClass(int id)
        {
          if (_context.SchoolClasses == null)
          {
              return NotFound();
          }
            var schoolClass = await _context.SchoolClasses.FindAsync(id);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return schoolClass;
        }
         
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolClass(int id, SchoolClass schoolClass)
        {
            if (id != schoolClass.Id)
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
                if (!SchoolClassExists(id))
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
         
        
/*        COMPLETED
*/        [HttpPost]
        public async Task<ActionResult<SchoolClassDTO>> PostSchoolClass(SchoolClassDTO schoolClass)
        {
          if (_context.SchoolClasses == null)
                return Problem("Entity set 'SchoolClasses' is null.");
          var realSchoolClass=_mapper.Map<SchoolClass>(schoolClass);
            
            _context.SchoolClasses.Add(realSchoolClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolClass", new { id = schoolClass.Id }, schoolClass);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolClass(int id)
        {
            if (_context.SchoolClasses == null)
            {
                return NotFound();
            }
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolClassExists(int id)
        {
            return (_context.SchoolClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
