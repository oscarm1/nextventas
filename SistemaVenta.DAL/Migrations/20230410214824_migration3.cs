using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateFin",
                table: "Subscriptions",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Subscriptions",
                newName: "ExpiryDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Subscriptions",
                newName: "DateFin");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Subscriptions",
                newName: "CreationDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModificationDate",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
