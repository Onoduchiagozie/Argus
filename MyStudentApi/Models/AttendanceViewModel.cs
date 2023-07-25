using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyStudentApi.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }
        [AllowNull]
        public int? SchoolClassId { get; set;}
        [ForeignKey("SchoolClassId")]
        public SchoolClass SchoolClass { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }    
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsRegistered { get; set; }
    }
}
