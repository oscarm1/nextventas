using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstmg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBook_Guests_IdGuestNavigationIdGuest",
                table: "DetailBook");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailBook_Producto",
                table: "DetailBook");

            migrationBuilder.DropForeignKey(
                name: "FK__DetalleBo__idBoo__440B1D61",
                table: "DetailBook");

            migrationBuilder.DropIndex(
                name: "IX_DetailBook_IdGuestNavigationIdGuest",
                table: "DetailBook");

            migrationBuilder.DropColumn(
                name: "IdGuestNavigationIdGuest",
                table: "DetailBook");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_IdGuest",
                table: "DetailBook",
                column: "IdGuest");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBook_Guest",
                table: "DetailBook",
                column: "IdGuest",
                principalTable: "Guests",
                principalColumn: "IdGuest",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBook_Room",
                table: "DetailBook",
                column: "idRoom",
                principalTable: "Rooms",
                principalColumn: "IdRoom",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__DetailBo__idBoo__440B1D61",
                table: "DetailBook",
                column: "idBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailBook_Guest",
                table: "DetailBook");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailBook_Room",
                table: "DetailBook");

            migrationBuilder.DropForeignKey(
                name: "FK__DetailBo__idBoo__440B1D61",
                table: "DetailBook");

            migrationBuilder.DropIndex(
                name: "IX_DetailBook_IdGuest",
                table: "DetailBook");

            migrationBuilder.AddColumn<int>(
                name: "IdGuestNavigationIdGuest",
                table: "DetailBook",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_IdGuestNavigationIdGuest",
                table: "DetailBook",
                column: "IdGuestNavigationIdGuest");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBook_Guests_IdGuestNavigationIdGuest",
                table: "DetailBook",
                column: "IdGuestNavigationIdGuest",
                principalTable: "Guests",
                principalColumn: "IdGuest");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailBook_Producto",
                table: "DetailBook",
                column: "idRoom",
                principalTable: "Rooms",
                principalColumn: "IdRoom",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__DetalleBo__idBoo__440B1D61",
                table: "DetailBook",
                column: "idBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
