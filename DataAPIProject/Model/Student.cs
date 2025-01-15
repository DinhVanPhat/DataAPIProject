using System.ComponentModel.DataAnnotations;

namespace DataAPIProject.Model;
public class Student
{
    [Key]
    public int ID { get; set; } // Primary Key
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Navigation Property
    public ICollection<Enrollment> Enrollments { get; set; }
}
