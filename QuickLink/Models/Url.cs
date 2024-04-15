using System.ComponentModel.DataAnnotations;

namespace QuickLink.Models
{
    public class Url
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Url]
        [MaxLength(512)]
        public string OriginalUrl { get; set; }

        [StringLength(8)]
        [Required]
        public string Code { get; set; }


        [Required]
        [Url]
        public string ShortenedUrl { get; set; }



        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }
    }
}
