using DataAPIProject.Model;
using Microsoft.EntityFrameworkCore;
using DataAPIProject.AppDbContext;

namespace DataAPIProject.Services
{
    public class CourseService
    {
        private readonly AppDBContext _context;

        public CourseService(AppDBContext context)
        {
            _context = context;
        }

        // Get all courses
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.FromResult(_context.Courses.ToList());
        }

        // Get course by ID
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }
            return await Task.FromResult(course);
        }

        // Create a new course
        public async Task<Course> CreateCourseAsync(Course newCourse)
        {
            if (_context.Courses.Any(c => c.CourseID == newCourse.CourseID))
            {
                throw new ArgumentException("Course with the same ID already exists.");
            }

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();
            return newCourse;
        }

        // Update an existing course
        public async Task<Course> UpdateCourseAsync(int id, Course updatedCourse)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }

            course.Title = updatedCourse.Title;
            course.Credits = updatedCourse.Credits;

            await _context.SaveChangesAsync();
            return course;
        }

        // Delete a course
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return false; 
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
