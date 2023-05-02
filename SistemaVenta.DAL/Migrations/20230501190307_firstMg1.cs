using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstMg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBook_Books_IdBookNavigationIdBook",
                table: "DetailBook");

            migrationBuilder.DropIndex(
                name: "IX_DetailBook_IdBookNavigationIdBook",
                table: "DetailBook");

            migrationBuilder.DropColumn(
                name: "IdBookNavigationIdBook",
                table: "DetailBook");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_idBook",
                table: "DetailBook",
                column: "idBook");

            migrationBuilder.AddForeignKey(
                name: "FK__DetailBo__idBoo",
                table: "DetailBook",
                column: "idBook",
                principalTable: "Books",
                principalColumn: "idBook",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__DetailBo__idBoo",
                table: "DetailBook");

            migrationBuilder.DropIndex(
                name: "IX_DetailBook_idBook",
                table: "DetailBook");

            migrationBuilder.AddColumn<int>(
                name: "IdBookNavigationIdBook",
                table: "DetailBook",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_IdBookNavigationIdBook",
                table: "DetailBook",
                column: "IdBookNavigationIdBook");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBook_Books_IdBookNavigationIdBook",
                table: "DetailBook",
                column: "IdBookNavigationIdBook",
                principalTable: "Books",
                principalColumn: "idBook");
        }
    }
}
