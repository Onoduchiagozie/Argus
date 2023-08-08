using MyStudentApi.Data;
using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface IEMailServices
    {
        bool SendEmail(Student student, SchoolClass schoolClass);
    }
}
