using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
/*        public ICollection<Student> Students { get; set; }
*/        public string? ClasssName { get; set; }
        public string? TeacherName { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public int UnitLoad { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }


        /*  public DateTime? Period { get; set; } = default(DateTime?);
  public TimeOnly? TimeOnly { get; set; }*/

    }
}
