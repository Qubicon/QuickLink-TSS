using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickLink.Database;
using QuickLink.Dtos;
using QuickLink.Models;
using QuickLink.Services;

namespace QuickLink.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UrlsController(QLDbContext dbContext, UrlShorteningService urlShorteningService) : ControllerBase
    {
        private readonly QLDbContext _dbContext = dbContext;
        private readonly UrlShorteningService _urlShorteningService = urlShorteningService;

        [HttpGet("urls")]
        public IActionResult GetAllUrls()
        {
            var urls = _dbContext.UrlsTable.ToList();
            return Ok(urls);
        }

        [HttpPost("url")]
        public async Task<ActionResult<UrlResponseDto>> ShortenUrl([FromBody] UrlCreationRequestDto urlCreationRequest)
        {
            var randomCode = await _urlShorteningService.GenerateRandomString();

            var url = new Url
            {
                OriginalUrl = urlCreationRequest.OriginalUrl,
                Code = randomCode,
                ShortenedUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/v1/{randomCode}"
            };

            _dbContext.UrlsTable.Add(url);
            await _dbContext.SaveChangesAsync();

            var responseDto = new UrlResponseDto
            {
                OriginalUrl = url.OriginalUrl,
                ShortenedUrl = url.ShortenedUrl
            };

            return Ok(responseDto);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string code)
        {
            var url = await _dbContext.UrlsTable.FirstOrDefaultAsync(s => s.Code == code);

            if (url == null)
            {
                return NotFound();
            }
            return Redirect(url.OriginalUrl);
        }

    }
}
