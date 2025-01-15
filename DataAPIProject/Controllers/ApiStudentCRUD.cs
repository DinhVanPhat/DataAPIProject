using DataAPIProject.Model;
using DataAPIProject.Services;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(new
                {
                    code = 0,
                    status = "success",
                    data = students
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = ex.Message
                });
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudent(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);

                return Ok(new
                {
                    code = 0,
                    status = "success",
                    data = student
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Student not found"
                });
            }
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult> PostStudent([FromBody] StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                });
            }

            var newStudent = new Student
            {
                LastName = studentDto.LastName,
                FirstMidName = studentDto.FirstMidName,
                EnrollmentDate = DateTime.SpecifyKind(studentDto.EnrollmentDate, DateTimeKind.Utc)
            };

            var createdStudent = await _studentService.CreateStudentAsync(newStudent);

            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.ID }, new
            {
                code = 0,
                status = "success",
                data = createdStudent
            });
        }



        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                });
            }

            var updatedStudent = new Student
            {
                ID = id,
                LastName = studentDto.LastName,
                FirstMidName = studentDto.FirstMidName,
                EnrollmentDate = DateTime.SpecifyKind(studentDto.EnrollmentDate, DateTimeKind.Utc)

            };

            try
            {
                var result = await _studentService.UpdateStudentAsync(id, updatedStudent);

                return Ok(new
                {
                    code = 0,
                    status = "success",
                    data = result
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    description = "Student not found"
                });
            }
        }



        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Student not found"
                });
            }

            return Ok(new
            {
                code = 0,
                status = "success",
                data = new { }
            });
        }
    }
}
