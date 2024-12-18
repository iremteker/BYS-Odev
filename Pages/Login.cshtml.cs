using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Migrations;

namespace SchoolManagementSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // DbContext'i dependency injection ile alıyor
        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? Message { get; set; }

        public string Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Message = "E-posta ve sifre gereklidir.";
                return Page();
            }

            // Kullanıcıyı veritabanında e-posta yoluyla arama işlemi
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user != null)
            {
                // şifreyi doğrulama
                var passwordHasher = new PasswordHasher<User>();
                if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, Password) == PasswordVerificationResult.Success)
                {
                    // Kullanıcı doğrulandı, rolüne göre yönlendirme yapma işlemi
                    if (user.Role == "Advisor")
                    {
                        // Personel giriş yaptı
                        return RedirectToPage("/Personnel/PersonnelDashboard");
                    }
                    else if (user.Role == "Student")
                    {
                        // Öðrenci giriş yaptı
                        return RedirectToPage("/Student/StudentDashboard");
                    }
                }
            }

            // Geçersiz e-posta veya şifre
            Message = "Geçersiz e-posta veya sifre.";
            return Page();
        }
    }
}
