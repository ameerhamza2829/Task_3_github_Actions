using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class SemesterRegistration
    {
        [Key]
        public int SemesterRegistrationID { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentID { get; set; }

        [Required]
        public int SemesterNumber { get; set; }

        [Required]
        [ForeignKey("Session")]
        public int SessionID { get; set; }
    }
}
