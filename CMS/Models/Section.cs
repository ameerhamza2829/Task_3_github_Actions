using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }

        [Required]
        [StringLength(100)]
        public string SectionName { get; set; }
    }
}
