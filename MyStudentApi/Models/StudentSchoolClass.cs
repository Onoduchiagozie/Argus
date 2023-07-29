namespace MyStudentApi.Models
{
        public class StudentSchoolClass
        {
            public int StudentId { get; set; }
            public Student Student { get; set; }

            public int SchoolClassId { get; set; }
            public SchoolClass SchoolClass { get; set; }
        }
    }

