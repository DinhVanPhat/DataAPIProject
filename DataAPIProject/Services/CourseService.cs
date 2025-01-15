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

        // API success
        private object ApiResponseSuccess(object data)
        {
            return new
            {
                code = 0,
                status = "success",
                data
            };
        }

        // API fail
        private object ApiResponseFail(string description)
        {
            return new
            {
                code = 1,
                status = "fail",
                data = new { },
                description
            };
        }

        // Get all courses
        public async Task<object> GetAllCoursesAsync()
        {
            try
            {
                var courses = await Task.FromResult(_context.Courses.ToList());
                return ApiResponseSuccess(courses);
            }
            catch (Exception ex)
            {
                return ApiResponseFail(ex.Message);
            }
        }

        // Get course by ID
        public async Task<object> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
                if (course == null)
                {
                    return ApiResponseFail("Course not found.");
                }

                return ApiResponseSuccess(course);
            }
            catch (Exception ex)
            {
                return ApiResponseFail(ex.Message);
            }
        }


        // Create a new course
        public async Task<object> CreateCourseAsync(Course newCourse)
        {
            try
            {
                if (_context.Courses.Any(c => c.CourseID == newCourse.CourseID))
                {
                    return ApiResponseFail("Course with the same ID already exists.");
                }
                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();

                return ApiResponseSuccess(newCourse);
            }
            catch (ArgumentException ex)
            {
                return ApiResponseFail(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseFail(ex.Message);
            }
        }

        // Update an existing course
        public async Task<object> UpdateCourseAsync(int id, Course updatedCourse)
        {
            try
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
                if (course == null)
                {
                    return ApiResponseFail("Course not found.");
                }

                course.Title = updatedCourse.Title;
                course.Credits = updatedCourse.Credits;

                await _context.SaveChangesAsync();

                return ApiResponseSuccess(course);
            }
            catch (Exception ex)
            {
                return ApiResponseFail(ex.Message);
            }
        }

        // Delete a course
        public async Task<object> DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return ApiResponseFail("Course not found.");
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return ApiResponseSuccess(new { });
            }
            catch (Exception ex)
            {
                return ApiResponseFail(ex.Message);
            }
        }
    }
}
