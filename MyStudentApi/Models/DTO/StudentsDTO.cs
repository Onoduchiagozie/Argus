using System.ComponentModel.DataAnnotations.Schema;

namespace MyStudentApi.Models.DTO
{
    public class StudentsDTO
    {
        public int Id { get; set; }
        public string? RegNo { get; set; }
        public string Email { get; set; }   
        public string? FullName { get; set; }
        [NotMapped]
        public List<int> CourseCodes { get; set; }
    }
}
