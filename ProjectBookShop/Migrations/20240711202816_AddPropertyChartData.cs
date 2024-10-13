using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyChartData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartData_Publisher_PublisherID",
                table: "ChartData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChartData",
                table: "ChartData");

            migrationBuilder.RenameTable(
                name: "ChartData",
                newName: "ChartDatas");

            migrationBuilder.RenameIndex(
                name: "IX_ChartData_PublisherID",
                table: "ChartDatas",
                newName: "IX_ChartDatas_PublisherID");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "ChartDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChartDatas",
                table: "ChartDatas",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartDatas_Publisher_PublisherID",
                table: "ChartDatas",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartDatas_Publisher_PublisherID",
                table: "ChartDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChartDatas",
                table: "ChartDatas");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "ChartDatas");

            migrationBuilder.RenameTable(
                name: "ChartDatas",
                newName: "ChartData");

            migrationBuilder.RenameIndex(
                name: "IX_ChartDatas_PublisherID",
                table: "ChartData",
                newName: "IX_ChartData_PublisherID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChartData",
                table: "ChartData",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartData_Publisher_PublisherID",
                table: "ChartData",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "PublisherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
