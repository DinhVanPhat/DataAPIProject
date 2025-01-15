using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPIProject.Model;
public class Enrollment
{
    [Key]
    public int EnrollmentID { get; set; } // Primary Key

    // Foreign Keys
    [ForeignKey(nameof(Course))] // Khóa ngoại liên kết với bảng Course
    public int CourseID { get; set; }

    [ForeignKey(nameof(Student))] // Khóa ngoại liên kết với bảng Student
    public int StudentID { get; set; }
    public string Grade { get; set; }

    // Navigation Properties
    public Course Course { get; set; }
    public Student Student { get; set; }
}
