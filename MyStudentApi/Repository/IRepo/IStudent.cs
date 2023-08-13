using MyStudentApi.Models;
using MyStudentApi.Models.DTO;

namespace MyStudentApi.Repository.IRepo
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetAllStudentsc();
        Task<IEnumerable<Student>> GetSchoolClassAsync(int courseCode);
        Task<bool> UpdateSchoolClassAsync(int courseCode, SchoolClass schoolClass);
        Task<Student> CreateStudents(Student students);
        Task<bool> DeleteSchoolClassAsync(int courseCode);
        bool SchoolClassExists(int courseCode);
    }
}
