namespace SchoolManagementSystem.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public bool IsMandatory { get; set; }
        public bool IsElective { get; set; }
        public int Credit { get; set; }
        public string Department { get; set; } = null!;


        // Navigasyon Özellikleri
        public ICollection<StudentCourseSelection> StudentCourseSelections { get; set; } = new List<StudentCourseSelection>(); // Dersin öğrenci seçimleri
    }
}
