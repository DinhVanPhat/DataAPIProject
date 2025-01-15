using System.ComponentModel.DataAnnotations;

namespace DataAPIProject.Model
{
    public class StudentDto
    {
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "FirstMidName is required.")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "EnrollmentDate is required.")]
        [DataType(DataType.Date, ErrorMessage = "EnrollmentDate must be a valid date.")]
        public DateTime EnrollmentDate { get; set; }
    }
}
