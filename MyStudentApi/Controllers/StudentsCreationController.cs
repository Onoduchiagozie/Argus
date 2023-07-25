﻿using System;
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
using MyStudentApi.Repository.IRepo;

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

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
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

            return student;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
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
                if (!StudentExists(id))
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
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            /*var today = DateTime.Now;
            var currentLecture = _context.SchoolClasses.FirstOrDefault(lecture =>
                                                                            (lecture.DayOfWeek == today.DayOfWeek) && 
                                                                            (lecture.StartTime.TimeOfDay <= today.TimeOfDay
                                                                             &&
                                                                             lecture.StopTime.TimeOfDay >= today.TimeOfDay));
            Student student1 = _mapper.Map<Student>(student);*/

           
/*                student1.SchoolClass = (SchoolClass?)currentLecture;
*//*                student1.IsRegistered = true;
*/                _context.Students.Add(student);
                Console.WriteLine("Successfully Saved Student Object.......................................");
       /*     }
            else
            {
                student1.IsRegistered = false;
                _context.Students.Add(student1);
            }*/
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

      
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

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
