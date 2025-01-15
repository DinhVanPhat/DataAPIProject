using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAPIProject.Model;
using DataAPIProject.Services;

namespace DataAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiCourseCRUD : ControllerBase
    {
        private readonly CourseService _courseService;

        public ApiCourseCRUD(CourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: api/apicoursecrud
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var response = _courseService.GetAllCoursesAsync().Result;
            return StatusCode(200, response);
        }
        // GET: api/apicoursecrud/4
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var response = _courseService.GetCourseByIdAsync(id).Result;
            return StatusCode(200, response);
        }
        // POST: api/apicoursecrud
        [HttpPost]
        public IActionResult PostCourse([FromBody] CourseDto courseDto)
        {
            // Kiểm tra xem trường dữ liệu có hợp lệ không
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    description = "CourseID, Title, and Credits must be provided."
                });
            }

            var newCourse = new Course
            {
                CourseID = courseDto.CourseID,
                Title = courseDto.Title,
                Credits = courseDto.Credits,
            };

            var response = _courseService.CreateCourseAsync(newCourse).Result;
            return StatusCode(200, response);
        }
        // PUT: api/apicoursecrud/4
        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, [FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    description = "CourseID, Title, and Credits must be provided."
                });
            }

            var updateCourse = new Course
            {
                CourseID = courseDto.CourseID,
                Title = courseDto.Title,
                Credits = courseDto.Credits,
            };

            var response = _courseService.UpdateCourseAsync(id, updateCourse).Result;
            return StatusCode(200, response);
        }
        // DELETE: api/apicoursecrud/4
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var response = _courseService.DeleteCourseAsync(id).Result;
            return StatusCode(200, response);
        }
    }

}
