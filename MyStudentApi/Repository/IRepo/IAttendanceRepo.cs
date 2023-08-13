using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface IAttendanceRepo
    {
        Task<IEnumerable<AttendanceViewModel>> GetAttendanceViewModels();
        Task<List<AttendanceViewModel>> GetAttendanceViewModel(int courseCode);
        Task<string> PostAttendanceViewModel(string studentRegNo);
        Task<bool> DeleteAttendanceViewModel(int CourseCode);
    }
}
