using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Batch
    {
        [Key]
        public int BatchID { get; set; }

        [Required]
        [StringLength(100)]
        public string BatchName { get; set; }
    }
}
