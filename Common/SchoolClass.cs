using MyStudentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SchoolClass
    {
        public ICollection<Student>? Students { get; set; } = new List<Student>();

        public int CourseCode { get; set; }

        public string? ClasssName { get; set; }
        public ICollection<AttendanceViewModel> AttendanceViewModel { get; set; } = new List<AttendanceViewModel>();

        public string? TeacherName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public int UnitLoad { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }

    }
}
