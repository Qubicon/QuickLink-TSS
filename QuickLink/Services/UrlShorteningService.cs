using Microsoft.EntityFrameworkCore;
using QuickLink.Database;

namespace QuickLink.Services
{
    public class UrlShorteningService
    {
        private readonly QLDbContext _dbContext;

        public UrlShorteningService(QLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string generatedCode;

            bool codeExists;
            do
            {
                generatedCode = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                // Check if the generated URL already exists in the database
                codeExists = await _dbContext.UrlsTable.AnyAsync(s => s.Code == generatedCode);

                // If the URL already exists, generate a new one
            } while (codeExists);

            return generatedCode;
        }
    }
}
