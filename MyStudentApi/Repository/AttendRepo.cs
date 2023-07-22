using AutoMapper.Features;
using Microsoft.EntityFrameworkCore;
using MyStudentApi.Data;
using MyStudentApi.Models;
using MyStudentApi.Repository.IRepo;
using System.Linq;

namespace MyStudentApi.Repository
{
    public class AttendRepo : IAttendance
    {
        private readonly TendancyDbContext _context;

        public AttendRepo(TendancyDbContext context)
        {
            _context = context;
        }
     

        public ICollection<AttendanceViewModel> GetAllAttendance()
        {
            
            var mylist= _context.AttendanceViewModel.Include(s=>s.Student).ToList();
            return mylist;
        }

        public AttendanceViewModel GetAttendace(int attendanceRecord)
        {
            var sheet = _context.AttendanceViewModel.FirstOrDefault(x=>x.Id == attendanceRecord);
            return sheet;
        }

        public bool StudentExists(string Regno)
        {
            var student = _context.Students.Any(x => x.RegNo == Regno);
            return student;
        }

        public bool SubmitAttendance(string Id)
        {

            /*    WRITE DOUBLE SIGN-IN PREVENTION*/
            var today=DateTime.Now;
            var student = _context.Students.FirstOrDefault(c => c.RegNo == Id);
            
            if (student == null)
            {
                return false;
            }

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            DayOfWeek currentDay = DateTime.Today.DayOfWeek;

            var currentLecture = _context.SchoolClasses.FirstOrDefault(lecture =>
                        lecture.DayOfWeek == currentDay &&
                        lecture.StartTime.TimeOfDay <= currentTime &&
                        lecture.StopTime.TimeOfDay >= currentTime.Add(TimeSpan.FromHours(2)));


/*MY SHITTY CODE*/
      /*      var ClassDay = _context.SchoolClasses.Where(c=> c.DayOfWeek == today.DayOfWeek);
            var classtime= _context.SchoolClasses.Where(obj =>obj.StartTime.Hour <= today.Hour
                                                                        && 
                                                                       obj.StopTime.Hour >= today.Hour);*/
           /* Common CLASS SECTION*/
   /*         var ongoing = ClassDay.Intersect(classtime);
            var hasCommonMembers = ClassDay.Any(obj1 => classtime.Any(obj2 =>
                                            obj1.StartTime == obj2.StartTime &&
                                            obj1.StopTime == obj2.StopTime &&
                                            obj1.UnitLoad == obj2.UnitLoad &&
                                            obj1.DayOfWeek == obj2.DayOfWeek));*/

            AttendanceViewModel attendanceViewModel = new AttendanceViewModel()
            {
                Student = student,
                SchoolClass = (SchoolClass)currentLecture,
            };

            _context.AttendanceViewModel.Add(attendanceViewModel);
            return Save();
          
        }


        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    
    }
}
