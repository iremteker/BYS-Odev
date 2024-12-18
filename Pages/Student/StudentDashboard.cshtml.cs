using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Controllers;
using Microsoft.EntityFrameworkCore; // Entity Framework için gerekli
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagementSystem.Data.Migrations;

namespace SchoolManagementSystem.Pages.Student
{
    public class StudentDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string FirstName { get; set; } = "Ali Veli";
        public List<Course> CoursesList { get; set; } = new List<Course>();


        public async Task OnGetAsync()
        {
            // Courses tablosundan tüm verileri çekme
            CoursesList = await _context.Courses.ToListAsync();

            if (CoursesList == null || CoursesList.Count == 0)
            {
                Console.WriteLine("Ders listesi boþ veya yüklenemedi.");
            }
        }
    }
}
