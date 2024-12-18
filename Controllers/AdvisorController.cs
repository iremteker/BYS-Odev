using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagementSystem.Data.Migrations;

namespace SchoolManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdvisorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/advisor/students
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET api/advisor/students/{id}
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Öğrenci bulunamadı.");
            }

            return Ok(student);
        }

        // POST api/advisor/students
        [HttpPost("students")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Geçersiz öğrenci bilgisi.");
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, student);
        }


        // PUT api/advisor/students/{id}
        [HttpPut("students/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest("ID uyumsuzluğu.");
            }

            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound("Güncellenecek öğrenci bulunamadı.");
            }

            // Öğrenci bilgilerini güncelle
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Email = student.Email;

            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();

            return NoContent(); // Güncelleme başarılı
        }
        // DELETE api/advisor/students/{id}
        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Silinecek öğrenci bulunamadı.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent(); // Silme başarılı
        }
    }
}
