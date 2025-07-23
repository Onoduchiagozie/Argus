using MyStudentApi.Models;
using MyStudentApi.Models.DTO;

namespace MyStudentApi.Repository.IRepo
{
    public interface ILecturesRepo
    {
         Task<SchoolClass> CreateLectures(SchoolClass studentsDTO);
          Task<List<SchoolClass>> Getlectures();
         Task<List<AttendanceViewModel>> BringLecturesAttendance(int coursecode);
    }
}
