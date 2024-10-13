using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Customers",
                newName: "PostalCode2");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publisher",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode1",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode1",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PostalCode2",
                table: "Customers",
                newName: "Mobile");

            migrationBuilder.AlterColumn<string>(
                name: "PublisherName",
                table: "Publisher",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Customers",
                type: "nvarChar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Customers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
