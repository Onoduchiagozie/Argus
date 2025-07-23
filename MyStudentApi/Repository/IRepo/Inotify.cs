using MyStudentApi.Models;

namespace MyStudentApi.Repository.IRepo
{
    public interface Inotify
    {
     public bool Notify(SchoolClass schoolClass);
     
     void NotifyAbsentees(List<Student> absentStudents, SchoolClass currentLecture);

    }
}
