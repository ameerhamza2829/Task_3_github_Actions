using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(20)]
        public string CourseCode { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        public int CreditHours { get; set; }
    }
}
