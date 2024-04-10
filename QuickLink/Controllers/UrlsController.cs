using Microsoft.AspNetCore.Mvc;
using QuickLink.Database;
using QuickLink.Dtos;
using QuickLink.Models;

namespace QuickLink.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly QLDbContext _dbContext;

        public UrlsController(QLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("urls")]
        public IActionResult GetAllUrls()
        {
            var urls = _dbContext.UrlsTable.ToList();
            return Ok(urls);
        }

        [HttpPost("short")]
        public async Task<ActionResult<UrlDto>> ShortenUrl(UrlDto urlRequest)
        {
            // TODO 
            var shortenedUrl = GenerateRandomString(8);

            var url = new Url
            {
                OriginalUrl = urlRequest.OriginalUrl,
                ShortenedUrl = shortenedUrl,
            };

            _dbContext.UrlsTable.Add(url);
            await _dbContext.SaveChangesAsync();

            var responseDto = new UrlDto
            {
                OriginalUrl = url.OriginalUrl,
                ShortenedUrl = url.ShortenedUrl
            };

            return Ok(responseDto);
        }

        // Helper method to generate a random string for shortened URL
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
