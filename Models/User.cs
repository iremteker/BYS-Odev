namespace SchoolManagementSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int RelatedID { get; set; }
        public string Email { get; set; } = null!;
    }
}
