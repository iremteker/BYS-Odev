namespace SchoolManagementSystem.Models
{
    public class Advisor : User
    {
        public int AdvisorID { get; set; }
        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Students { get; set; } = null!;

        public ICollection<Student> Student { get; set; } = new List<Student>();

    }
}
