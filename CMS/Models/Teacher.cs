using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [Required]
        [StringLength(20)]
        public string TeacherCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Range(1, 150)]
        public int Age { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
    }
}
