using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface ICreateStudent
    {
        ICollection<Student> GetAllStudents();
        Student RegisterStudent(Student student);
        Student StudentExists(string Id);
        
        bool RemoveStudent(string Id);
        bool Save();
    }
}
