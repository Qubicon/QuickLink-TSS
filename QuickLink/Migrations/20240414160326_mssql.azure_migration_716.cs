using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickLink.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_716 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UrlsTable_ShortenedUrl",
                table: "UrlsTable",
                column: "ShortenedUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UrlsTable_ShortenedUrl",
                table: "UrlsTable");
        }
    }
}
