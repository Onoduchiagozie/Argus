using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? RegNo { get; set; }   
        public string? FullName { get; set; }
 
        
    }
}
