using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class AddBook_CategryTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Translator_BookInfo_BookID",
                table: "Book_Translator");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Translator_Translator_TranslatorID",
                table: "Book_Translator");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Translator",
                table: "Book_Translator");

            migrationBuilder.RenameTable(
                name: "Book_Translator",
                newName: "Book_Translators");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Translator_TranslatorID",
                table: "Book_Translators",
                newName: "IX_Book_Translators_TranslatorID");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "BookInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "BookInfo",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublishe",
                table: "BookInfo",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublisheDate",
                table: "BookInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisheYear",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Translators",
                table: "Book_Translators",
                columns: new[] { "BookID", "TranslatorID" });

            migrationBuilder.CreateTable(
                name: "Book_Ctegory",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Ctegory", x => new { x.BookID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_Book_Ctegory_BookInfo_BookID",
                        column: x => x.BookID,
                        principalTable: "BookInfo",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Ctegory_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Ctegory_CategoryID",
                table: "Book_Ctegory",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Translators_BookInfo_BookID",
                table: "Book_Translators",
                column: "BookID",
                principalTable: "BookInfo",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Translators_Translator_TranslatorID",
                table: "Book_Translators",
                column: "TranslatorID",
                principalTable: "Translator",
                principalColumn: "TranslatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Translators_BookInfo_BookID",
                table: "Book_Translators");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Translators_Translator_TranslatorID",
                table: "Book_Translators");

            migrationBuilder.DropForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo");

            migrationBuilder.DropTable(
                name: "Book_Ctegory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Translators",
                table: "Book_Translators");

            migrationBuilder.DropColumn(
                name: "Delete",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "IsPublishe",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "PublisheDate",
                table: "BookInfo");

            migrationBuilder.DropColumn(
                name: "PublisheYear",
                table: "BookInfo");

            migrationBuilder.RenameTable(
                name: "Book_Translators",
                newName: "Book_Translator");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Translators_TranslatorID",
                table: "Book_Translator",
                newName: "IX_Book_Translator_TranslatorID");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Translator",
                table: "Book_Translator",
                columns: new[] { "BookID", "TranslatorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Translator_BookInfo_BookID",
                table: "Book_Translator",
                column: "BookID",
                principalTable: "BookInfo",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Translator_Translator_TranslatorID",
                table: "Book_Translator",
                column: "TranslatorID",
                principalTable: "Translator",
                principalColumn: "TranslatorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookInfo_Categories_CategoryID",
                table: "BookInfo",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
