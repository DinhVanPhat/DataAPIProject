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
        public async Task<IActionResult> GetAllStudents()
        {
            var response = await _studentService.GetAllStudentsAsync();
            return Ok(response);
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var response = await _studentService.DeleteStudentAsync(id);
            return Ok(response);
        }

    }
}
