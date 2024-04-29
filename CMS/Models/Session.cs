using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

        [Required]
        [StringLength(100)]
        public string SessionName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }

}
