using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class Add_Weight_ISBN_NumOfPage_TOBookIfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "BookInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumOfPages",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "weight",
                table: "BookInfo",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "NumOfPages",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "BookInfo");
        }
    }
}
