namespace DataAPIProject.Model
{
    public class Course
    {
        public int CourseID { get; set; } // Primary Key
        public string Title { get; set; }
        public int Credits { get; set; }

        // Navigation Property
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
