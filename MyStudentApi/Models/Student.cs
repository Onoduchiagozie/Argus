using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? RegNo { get; set; }   
        public string? FullName { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public int[] CourseCodes { get; set; }
        public ICollection<SchoolClass> SchoolClasses { get; set; }=new List<SchoolClass>();
/*        public List<int> EnrolledClassIds { get; set; }
*/        public ICollection<AttendanceViewModel> AttendanceViewModel { get; set; } = new List<AttendanceViewModel>();



     }
}
