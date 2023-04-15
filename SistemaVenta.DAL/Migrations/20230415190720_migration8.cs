using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameLogo",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "UrlLogo",
                table: "Establishments");

            migrationBuilder.AddColumn<string>(
                name: "NameImage",
                table: "Establishments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Establishments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameImage",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Establishments");

            migrationBuilder.AddColumn<string>(
                name: "NameLogo",
                table: "Establishments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlLogo",
                table: "Establishments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
