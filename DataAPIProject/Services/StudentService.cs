using DataAPIProject.Model;
using Microsoft.EntityFrameworkCore;
using DataAPIProject.AppDbContext;

namespace DataAPIProject.Services
{
    public class StudentService
    {
        private readonly AppDBContext _context;

        public StudentService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            // Logic xử lý dữ liệu
            return await _context.Students
                .Include(s => s.Enrollments) // Load dữ liệu liên quan
                .OrderByDescending(s => s.EnrollmentDate) // Sắp xếp theo ngày ghi danh
                .ToListAsync();
        }

        // Phương thức xóa sinh viên theo ID
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false; // Không tìm thấy sinh viên
            }

            // Xóa sinh viên khỏi DbContext
            _context.Students.Remove(student);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return true; // Xóa thành công
        }
    }
}
