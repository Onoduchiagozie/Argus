using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyStudentApi.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
         
        public int? SchoolClassId { get; set;}
        [ForeignKey("SchoolClassId")]
        public SchoolClass SchoolClass { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }    
        public string? Course { get; set; }
        public DateTime? StopTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsRegistered { get; set; }

        public static explicit operator AttendanceViewModel(List<AttendanceViewModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
