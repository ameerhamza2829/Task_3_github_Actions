using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(20)]
        public string RollNo { get; set; }

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

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(20)]
        public string PhoneNo { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [ForeignKey("Batch")]
        public int BatchID { get; set; }

        [Required]
        [ForeignKey("Campus")]
        public int CampusID { get; set; }

        [Required]
        [ForeignKey("Section")]
        public int SectionID { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
    }
}
