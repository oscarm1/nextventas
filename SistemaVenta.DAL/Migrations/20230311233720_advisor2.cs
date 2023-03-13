using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class advisor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyOnNotFound",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "UrlImagen",
                table: "Rooms",
                newName: "UrlImage");

            migrationBuilder.RenameColumn(
                name: "TimeInActivity",
                table: "Rooms",
                newName: "Capacity");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Rooms",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "Rooms",
                newName: "UrlImagen");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Rooms",
                newName: "TimeInActivity");

            migrationBuilder.AddColumn<string>(
                name: "ReplyOnNotFound",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
