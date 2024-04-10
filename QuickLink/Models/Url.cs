using System.ComponentModel.DataAnnotations;

namespace QuickLink.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Url]
        [MaxLength(256)]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(8)]
        public string ShortenedUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
