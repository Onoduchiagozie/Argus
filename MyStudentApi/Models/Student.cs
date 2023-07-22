using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? RegNo { get; set; }   
        public string? Name { get; set; }
        public int? SchoolClassId { get; set; }
        
        public SchoolClass? SchoolClass { get; set; }
        public bool IsRegistered { get; set; }
        
    }
}
