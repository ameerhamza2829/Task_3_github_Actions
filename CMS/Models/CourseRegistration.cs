using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class CourseRegistration
    {
        [Key]
        public int CourseRegistrationID { get; set; }

        [Required]
        public int SemesterRegistrationID { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Required]
        [ForeignKey("Session")]
        public int SessionID { get; set; }

        [Required]
        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }

        public decimal MidPercentage { get; set; }

        public decimal FinalPercentage { get; set; }

        public decimal SessionalPercentage { get; set; }
    }
}
