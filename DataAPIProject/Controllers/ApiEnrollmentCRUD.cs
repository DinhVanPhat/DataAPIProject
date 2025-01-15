using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAPIProject.Model;
using DataAPIProject.Services;

namespace DataAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEnrollmentCRUDController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public ApiEnrollmentCRUDController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // GET: api/ApiEnrollmentCRUD
        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return _enrollmentService.SuccessResponse(enrollments);
        }

        // GET: api/ApiEnrollmentCRUD/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollment(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentAsync(id);
            if (enrollment == null)
            {
                return _enrollmentService.ErrorResponse("Enrollment not found");
            }

            return _enrollmentService.SuccessResponse(enrollment, "Here is your Enrollment you find!");
        }

        // POST: api/ApiEnrollmentCRUD
        [HttpPost]
        public async Task<IActionResult> PostEnrollment([FromBody] Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return _enrollmentService.ErrorResponse("Invalid input data");
            }

            var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollment);


            return _enrollmentService.SuccessResponse(createdEnrollment, string.Empty);
        }


        // PUT: api/ApiEnrollmentCRUD/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, [FromBody] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return _enrollmentService.ErrorResponse("Mismatched ID");
            }

            var updatedEnrollment = await _enrollmentService.UpdateEnrollmentAsync(enrollment);
            if (updatedEnrollment == null)
            {
                return _enrollmentService.ErrorResponse("Enrollment not found");
            }

            return _enrollmentService.SuccessResponse(updatedEnrollment, "Enrollment updated successfully");
        }

        // DELETE: api/ApiEnrollmentCRUD/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var (success, deletedEnrollment, errorMessage) = await _enrollmentService.DeleteEnrollmentAsync(id);

            if (!success)
            {
                return _enrollmentService.ErrorResponse(errorMessage);
            }

            return _enrollmentService.SuccessResponse(deletedEnrollment, "Enrollment deleted successfully");
        }

    }
}
