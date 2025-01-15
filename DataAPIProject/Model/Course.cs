using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAPIProject.Model
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; } // Primary Key
        public string Title { get; set; }
        public int Credits { get; set; }

        // Navigation Property
        [JsonIgnore] // Bỏ qua khi tuần tự hóa
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
