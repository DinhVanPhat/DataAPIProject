using System.ComponentModel.DataAnnotations;

namespace DataAPIProject.Model
{
    public class CourseDto
    {
        [Required(ErrorMessage = "CourseID is required.")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Credits is required.")]
        public int Credits { get; set; }
    }
}
