using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface IAttendance
    {
        ICollection<AttendanceViewModel> GetAllAttendance();
        AttendanceViewModel GetAttendace(int nationparkId);
        bool StudentExists(string name);
        bool SubmitAttendance(string Id);
        bool Save();

    }
}
