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
    
     public Task<Student> CreateStudents(Student student)
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
        public async Task<bool> DeleteSchoolClassAsync(int courseCode)
        {
            var classToDelete = await _context.SchoolClasses.FirstOrDefaultAsync(x => x.CourseCode == courseCode);
    
            if (classToDelete == null)
                return false;

            _context.SchoolClasses.Remove(classToDelete);
            await _context.SaveChangesAsync();
    
            return true;
        }


        public async Task<IEnumerable<Student>> GetAllStudents()
        {
         //   throw new NotImplementedException();
           var box =  await _context.Students.ToListAsync();
           return box;
        }

        public  async Task<IEnumerable<Student>> GetSchoolClassAsync(int courseCode)
        {
            var students = await _context.Students
                         .Where(student => student.SchoolClasses.Any(course => course.CourseCode == courseCode))
                         .ToListAsync();

            return students;
        }

        // async Task<IEnumerable<Student>> IStudent.GetAllStudentsc()
        // {
        //    var box= await  _context.Students.ToListAsync();
        //     return box;
        // }
        //
        public List<Student> GetStudentsForClass(int classId)
        {
            return _context.Students.Where(s => s.SchoolClasses.Any(sc => sc.Id == classId)).ToList();
        }

       public bool SchoolClassExists(int courseCode)
        {
         
           var box =  _context.SchoolClasses.AnyAsync(s => s.CourseCode == courseCode);
           return box.Result;
        }

        public void UpdateSchoolClassAsync(int courseCode, SchoolClass schoolClass)
        {
            _context.SchoolClasses.Update(schoolClass);
            _context.SaveChangesAsync();
        }
    }
}
