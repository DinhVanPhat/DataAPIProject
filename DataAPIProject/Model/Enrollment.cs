using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAPIProject.Model;
public class Enrollment
{
    [Key]
    public int EnrollmentID { get; set; } // Primary Key

    // Foreign Keys
    [Required]
    [ForeignKey(nameof(Course))] // Khóa ngoại liên kết với bảng Course
    public int CourseID { get; set; }
    [Required]
    [ForeignKey(nameof(Student))] // Khóa ngoại liên kết với bảng Student
    public int StudentID { get; set; }
    public string Grade { get; set; }

    // Navigation Properties
    [JsonIgnore]
    public Course? Course { get; set; }
    [JsonIgnore] // Bỏ qua khi tuần tự hóa
    public Student? Student { get; set; }
}
