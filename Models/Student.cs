namespace SchoolManagementSystem.Models
{
    public class Student : User
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int AdvisorID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Department { get; set; } = null!;

        // Navigasyon Özellikleri
        public Advisor Advisor { get; set; } = null!; // Öğrencinin danışmanı

        public ICollection<Student> StudentCourseSelection { get; set; } = new List<Student>(); // Öğrencinin ders seçimleri
    }
}
