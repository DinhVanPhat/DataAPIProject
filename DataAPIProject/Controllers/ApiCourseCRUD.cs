using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAPIProject.Model;
using DataAPIProject.Services;

namespace DataAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCourseCRUD : ControllerBase
    {
        private readonly CourseService _courseService;

        public ApiCourseCRUD(CourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult> GetAllCourse()
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync();
                var response = new
                {
                    code = 0,
                    status = "success",
                    data = courses
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = ex.Message
                };
                return StatusCode(500, response);
            }
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Course not found"
                });
            }

            return Ok(new
            {
                code = 0,
                status = "success",
                data = course
            });
        }



        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult> PostCourse(Course course)
        {
            var createdCourse = await _courseService.CreateCourseAsync(course);

            if (createdCourse == null)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Course creation failed"
                });
            }

            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseID }, new
            {
                code = 0,
                status = "success",
                data = createdCourse
            });
        }


        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseID)
            {
                return BadRequest(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Course ID mismatch."
                });
            }

            var updatedCourse = await _courseService.UpdateCourseAsync(id, course);

            if (updatedCourse == null)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Course not found"
                });
            }

            return Ok(new
            {
                code = 0,
                status = "success",
                data = updatedCourse
            });
        }


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    code = 1,
                    status = "fail",
                    data = new { },
                    description = "Course not found"
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
