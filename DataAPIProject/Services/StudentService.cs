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

        public async Task<object> GetAllStudentsAsync()
        {
            try
            {
                var students = await _context.Students
                    .Include(s => s.Enrollments)
                    .OrderByDescending(s => s.EnrollmentDate)
                    .ToListAsync();
                if (students.Count > 0)
                {
                    return new
                    {
                        code = 0,
                        status = "success",
                        data = students,
                        description = "Show list of successful students."
                    };
                }
                else
                {
                    return new
                    {
                        code = 1,
                        status = "fail",
                        data = students,
                        description = "No students found"
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    code = 1,
                    status = "fail",
                    data = (object)null,
                    description = $"Internal server error: {ex.Message}"
                };
            }
        }

        public async Task<object> DeleteStudentAsync(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return new
                    {
                        code = 1,
                        status = "fail",
                        data = (object)null,
                        description = "Student not found."
                    };
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return new
                {
                    code = 0,
                    status = "success",
                    data = student,
                    description = "Student deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    code = 1,
                    status = "fail",
                    data = (object)null,
                    description = $"Internal server error: {ex.Message}"
                };
            }
        }

    }
}
