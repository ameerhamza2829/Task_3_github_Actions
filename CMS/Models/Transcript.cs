using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Transcript
    {
        [Key]
        public int TranscriptID { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentID { get; set; }

        [Required]
        public int SemesterNumber { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [StringLength(10)]
        public string CourseGrade { get; set; }

        [StringLength(10)]
        public string SemesterGrade { get; set; }
    }
}
