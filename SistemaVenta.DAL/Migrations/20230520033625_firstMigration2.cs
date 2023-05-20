using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVenta.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Caja__idProveedor__403A8C7D",
                table: "Caja");

            migrationBuilder.CreateIndex(
                name: "IX_Caja_IdUsuario",
                table: "Caja",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK__Caja__idProveedor__403A8C7D",
                table: "Caja",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Caja__medioPago__403A8C7D",
                table: "Caja",
                column: "IdMedioPago",
                principalTable: "MedioPago",
                principalColumn: "idMedioPago",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Caja__idProveedor__403A8C7D",
                table: "Caja");

            migrationBuilder.DropForeignKey(
                name: "FK__Caja__medioPago__403A8C7D",
                table: "Caja");

            migrationBuilder.DropIndex(
                name: "IX_Caja_IdUsuario",
                table: "Caja");

            migrationBuilder.AddForeignKey(
                name: "FK__Caja__idProveedor__403A8C7D",
                table: "Caja",
                column: "IdMedioPago",
                principalTable: "MedioPago",
                principalColumn: "idMedioPago",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
