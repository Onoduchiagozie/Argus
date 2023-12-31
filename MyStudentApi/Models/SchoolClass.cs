﻿using Azure;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class SchoolClass
    {

        public int Id { get; set; }
        public ICollection<Student>? Students { get; set; } = new List<Student>();
        
         public int CourseCode { get; set; }

        public string? ClasssName { get; set; }
        public ICollection<AttendanceViewModel> AttendanceViewModel { get; set; } = new List<AttendanceViewModel>();

        public string? TeacherName { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
         public int UnitLoad { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }


        /*  public DateTime? Period { get; set; } = default(DateTime?);
  public TimeOnly? TimeOnly { get; set; }*/

    }
}
