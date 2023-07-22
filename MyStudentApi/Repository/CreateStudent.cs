using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Repository.IRepo;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace MyStudentApi.Repository
{
    public class CreateStudent : ICreateStudent
    {
        private readonly TendancyDbContext _context;

        public CreateStudent(TendancyDbContext context)
        {
            _context = context;
        }


        public ICollection<Student> GetAllStudents()
        {
            var box= _context.Students.ToList();
            return box;
 
        }

        public Student RegisterStudent(Student student)
        {
            return student;
        }

        public bool RemoveStudent(string Id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Student StudentExists(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
