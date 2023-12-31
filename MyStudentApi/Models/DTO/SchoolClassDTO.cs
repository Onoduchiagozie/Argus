﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models.DTO
{
    public class SchoolClassDTO
    {
        public int Id { get; set; }
        public string? ClasssName { get; set; }
        public string? TeacherName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public int CourseCode { get; set; }
        public int UnitLoad { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
/*        public List<int> CourseCodes { get; set; }
*/
    }
}


    /*  public DateTime? Period { get; set; } = default(DateTime?);
public TimeOnly? TimeOnly { get; set; }*/


