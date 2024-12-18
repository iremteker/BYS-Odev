using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Migrations;

namespace SchoolManagementSystem.Pages.Advisor
{
    public class AdvisorDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // DbContext'i enjekte etme
        public AdvisorDashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student NewStudent { get; set; } = new Student();

        public List<Student> StudentsList { get; set; } = new List<Student>();

        public string FullName { get; set; } = "Mehmet Yılmaz"; // Varsayılan değer

        //Sayfa yüklendiğinde tüm öğrencileri listeleme
        public async Task<IActionResult> OnGetAsync()
        {
            // Veritabanýndan tüm öðrencileri çekiyoruz
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }

        // Yeni öğrenci ekleme işlemi
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni öğrenci verisini veritabanına ekleme
                    _context.Students.Add(NewStudent);
                    await _context.SaveChangesAsync();

                    // Başarıyla eklendikten sonra sayfayı yeniden yükleme
                    return RedirectToPage("/Advisor/AdvisorDashboard");
                }
                catch (Exception ex)
                {
                    // Hata mesajı gösterme
                    ModelState.AddModelError(string.Empty, $"Öğrenci eklenirken bir hata oluştu: {ex.Message}");
                }
            }
            StudentsList = await _context.Students.ToListAsync();
            // Ekleme işleminde hata olduysa tekrar sayfayı döndür
            return Page();
        }

        // Öğrenci güncelleme
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                var existingStudent = await _context.Students.FindAsync(NewStudent.StudentID);
                if (existingStudent == null)
                {
                    ModelState.AddModelError(string.Empty, "Güncellenecek öðrenci bulunamadý.");
                    StudentsList = await _context.Students.ToListAsync();
                    return Page();
                }

                existingStudent.FirstName = NewStudent.FirstName;
                existingStudent.LastName = NewStudent.LastName;
                existingStudent.Email = NewStudent.Email;
                existingStudent.AdvisorID = NewStudent.AdvisorID;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Personnel/PersonnelDashboard");
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError(string.Empty, $"Veritabani güncelleme hatasi: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Güncelleme sirasinda hata olustu: {ex.Message}");
                }
            }

            // Listeyi güncel verilerle yükle
            StudentsList = await _context.Students.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnGetUpdateAsync(int id)
        {
            // Belirli bir öğrenciyi ID ile bulma
            NewStudent = await _context.Students.FindAsync(id);
            if (NewStudent == null)
            {
                return NotFound("Güncellenecek öðrenci bulunamadý.");
            }

            // Güncelleme formunu göstermek için sayfayı döndür
            return Page();
        }


        // Öğrenci silme işlemi
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Silinecek öðrenci bulunamadý.");
                StudentsList = await _context.Students.ToListAsync();
                return Page();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                // Silme işleminden sonra listeyi güncelleme
                StudentsList = await _context.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Öğrenci silinirken bir hata oluştu: {ex.Message}");
            }
            return Page();
        }
    }
}
