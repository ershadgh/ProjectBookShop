using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class changeNameFille : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file",
                table: "BookInfo",
                newName: "Fille");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fille",
                table: "BookInfo",
                newName: "file");
        }
    }
}
