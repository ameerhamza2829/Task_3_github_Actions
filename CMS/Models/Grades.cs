using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Grades
    {
        [Key]
        public int GradeID { get; set; }

        [Required]
        [ForeignKey("Session")]
        public int SessionID { get; set; }

        [Required]
        [StringLength(100)]
        public string GradeName { get; set; }

        [Required]
        public decimal MinPercentage { get; set; }

        [Required]
        public decimal MaxPercentage { get; set; }
    }
}
