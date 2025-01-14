using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAPIProject.AppDbContext;
using DataAPIProject.Services;

namespace DataAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiStudentCRUD : ControllerBase
    {
        private readonly StudentService _studentService;

        public ApiStudentCRUD(StudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                // Gọi phương thức xóa sinh viên từ service
                var result = await _studentService.DeleteStudentAsync(id);

                if (result)
                {
                    // Trả về mã 200 OK nếu xóa thành công
                    return Ok("Student deleted successfully.");
                }
                else
                {
                    // Trả về mã 404 nếu không tìm thấy sinh viên để xóa
                    return NotFound("Student not found.");
                }
            }
            catch (Exception ex)
            {
                // Trả về mã 500 Internal Server Error nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
