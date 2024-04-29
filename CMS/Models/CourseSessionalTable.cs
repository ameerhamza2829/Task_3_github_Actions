using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class CourseSessionalTable
    {
        [Key]
        public int SessionalID { get; set; }

        [Required]
        [ForeignKey("CourseRegistration")]
        public int CourseRegistrationID { get; set; }

        [Required]
        [StringLength(100)]
        public string SessionalName { get; set; }

        public decimal ObtainedMarks { get; set; }

        public decimal TotalMarks { get; set; }

        public decimal Weightage { get; set; }
    }
}
