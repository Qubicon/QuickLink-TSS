using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickLink.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_311 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "UrlsTable",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "UrlsTable");
        }
    }
}
