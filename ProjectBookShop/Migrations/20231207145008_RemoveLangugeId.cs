using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLangugeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Languages_LanguageID",
                table: "BookInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo");

            migrationBuilder.DropIndex(
                name: "IX_BookInfo_CategoryID",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "LanguagID",
                table: "BookInfo");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Languages_LanguageID",
                table: "BookInfo",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "LanguageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Languages_LanguageID",
                table: "BookInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "BookInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "BookInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "BookInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguagID",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookInfo_CategoryID",
                table: "BookInfo",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Languages_LanguageID",
                table: "BookInfo",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "LanguageID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Publisher_PublisherID",
                table: "BookInfo",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID");
        }
    }
}
