using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rnt",
                table: "Establishments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rnt",
                table: "Establishments");
        }
    }
}
