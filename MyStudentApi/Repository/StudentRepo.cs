using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;
using NuGet.Packaging;

namespace MyStudentApi.Repository.IRepo
{
    public class StudentRepo : IStudent
    {
        private readonly TendancyDbContext _context ;
         public StudentRepo(TendancyDbContext context) { 
            _context = context;
            
        }
    
     Task<Student> IStudent.CreateStudents(Student student)
        {
            

            var classesToAdd = _context.SchoolClasses.Where(c => student.CourseCodes.Contains(c.CourseCode)).ToList();
            if (classesToAdd.Count > 0)
            {
                var copyOfClassesToAdd = new List<SchoolClass>(classesToAdd);
                student.SchoolClasses.AddRange(copyOfClassesToAdd);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return Task.FromResult(student);
            
        }

        Task<bool> IStudent.DeleteSchoolClassAsync(int courseCode)
        {
            throw new NotImplementedException();
        }

         async Task<IEnumerable<Student>> IStudent.GetSchoolClassAsync(int courseCode)
        {
            var students = await _context.Students
                         .Where(student => student.SchoolClasses.Any(course => course.CourseCode == courseCode))
                         .ToListAsync();

            return students;
        }

        async Task<IEnumerable<Student>> IStudent.GetAllStudentsc()
        {
           var box= await  _context.Students.ToListAsync();
            return box;
        }

        bool IStudent.SchoolClassExists(int courseCode)
        {
            throw new NotImplementedException();
        }

        Task<bool> IStudent.UpdateSchoolClassAsync(int courseCode, SchoolClass schoolClass)
        {
            throw new NotImplementedException();
        }
    }
}
