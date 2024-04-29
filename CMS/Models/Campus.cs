using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Campus
    {
        [Key]
        public int CampusID { get; set; }

        [Required]
        [StringLength(100)]
        public string CampusName { get; set; }
    }
}
