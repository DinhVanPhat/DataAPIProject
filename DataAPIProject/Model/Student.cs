using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAPIProject.Model;
public class Student
{
    [Key]
    public int ID { get; set; } // Primary Key
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Navigation Property
    [JsonIgnore] // Bỏ qua khi tuần tự hóa
    public ICollection<Enrollment> Enrollments { get; set; }
}
