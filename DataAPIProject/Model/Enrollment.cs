namespace DataAPIProject.Model;
public class Enrollment
{
    public int EnrollmentID { get; set; } // Primary Key

    // Foreign Keys
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public string Grade { get; set; }

    // Navigation Properties
    public Course Course { get; set; }
    public Student Student { get; set; }
}
