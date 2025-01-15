using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAPIProject.Model;
using DataAPIProject.AppDbContext;

namespace DataAPIProject.Services
{
    public class EnrollmentService
    {
        private readonly AppDBContext _context;

        public EnrollmentService(AppDBContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        // Read
        public async Task<Enrollment> GetEnrollmentAsync(int id)
        {
            return await _context.Enrollments
                                 .Include(e => e.Course)
                                 .Include(e => e.Student)
                                 .FirstOrDefaultAsync(e => e.EnrollmentID == id);
        }

        // Update
        public async Task<Enrollment> UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        // Delete
        public async Task<(bool success, Enrollment deletedEnrollment, string errorMessage)> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return (false, null, "Enrollment not found");
            }
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return (true, enrollment, string.Empty); 
        }


        // Get All
        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollments
                                 .Include(e => e.Course)
                                 .Include(e => e.Student)
                                 .ToListAsync();
        }

        // Success Response
        public IActionResult SuccessResponse<T>(T data, string description = "")
        {
            var response = new
            {
                Code = 0,
                Status = "success",
                Data = data,
                Description = description
            };

            return new JsonResult(response); 
        }


        // Error Response
        public IActionResult ErrorResponse(string description, int code = 1)
        {
            return new BadRequestObjectResult(new
            {
                Code = code,
                Status = "fail",
                Data = (object)null,
                Description = description
            });
        }
    }
}
