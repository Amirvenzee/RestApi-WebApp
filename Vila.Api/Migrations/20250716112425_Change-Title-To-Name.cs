using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VILA.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTitleToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Vilas",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vilas",
                newName: "Title");
        }
    }
}
