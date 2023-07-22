using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
        public int SchoolClassId { get; set;}
        [ForeignKey("SchoolClassId")]
        public SchoolClass? SchoolClass { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }    
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
