using System.ComponentModel.DataAnnotations;

namespace QuickLink.Dtos
{
    public class UrlCreationRequestDto
    {
        [Required]
        public String OriginalUrl { get; set; }
    }
}
