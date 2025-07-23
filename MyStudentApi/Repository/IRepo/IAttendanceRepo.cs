using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface IAttendanceRepo
    {
   Task<bool> PostAttendanceAsync(AttendanceViewModel attendance);
        Task<List<AttendanceViewModel>> GetAttendanceByCourseCode(int courseCode);
        Task<List<AttendanceViewModel>> GetAllAttendance();
        Task<List<AttendanceViewModel>> GetTodaysAttendance(DateTime startTime, DateTime stopTime);
        Task<bool> DeleteAttendanceAsync(Guid attendanceId);
        Task<bool> AttendanceExistsAsync(int id);

        
 
        
        
        
        
        
    }
}
