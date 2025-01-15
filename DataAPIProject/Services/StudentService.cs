using DataAPIProject.AppDbContext;
using DataAPIProject.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAPIProject.Services
{
    public class StudentService
    {
        private readonly AppDBContext _context;

        public StudentService(AppDBContext context)
        {
            _context = context;
        }

        // Get all students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(s => s.Enrollments).ToListAsync();
        }

        // Get student by ID
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.Include(s => s.Enrollments)
                                                 .FirstOrDefaultAsync(s => s.ID == id);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            return student;
        }

        // Create a new student
        public async Task<Student> CreateStudentAsync(Student newStudent)
        {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }

        // Update an existing student
        public async Task<Student> UpdateStudentAsync(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            student.LastName = updatedStudent.LastName;
            student.FirstMidName = updatedStudent.FirstMidName;
            student.EnrollmentDate = updatedStudent.EnrollmentDate;

            await _context.SaveChangesAsync();
            return student;
        }

        // Delete a student
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
