using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickLink.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_878 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UrlsTable_ShortenedUrl",
                table: "UrlsTable");

            migrationBuilder.AlterColumn<string>(
                name: "ShortenedUrl",
                table: "UrlsTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "UrlsTable",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_UrlsTable_Code",
                table: "UrlsTable",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UrlsTable_Code",
                table: "UrlsTable");

            migrationBuilder.AlterColumn<string>(
                name: "ShortenedUrl",
                table: "UrlsTable",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "UrlsTable",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.CreateIndex(
                name: "IX_UrlsTable_ShortenedUrl",
                table: "UrlsTable",
                column: "ShortenedUrl",
                unique: true);
        }
    }
}
