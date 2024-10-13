using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class AddPubisherTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "BookInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInfo_PublisherID",
                table: "BookInfo",
                column: "PublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_BookInfo_PublisherID",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "BookInfo");
        }
    }
}
