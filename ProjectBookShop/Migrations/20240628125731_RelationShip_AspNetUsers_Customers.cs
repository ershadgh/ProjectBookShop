using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBookShop.Migrations
{
    /// <inheritdoc />
    public partial class RelationShip_AspNetUsers_Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("Fk_Customers_AspNetUsers_CustomserID",
                "Customers", "CustomserID", "AspNetUsers",
               principalColumn: "Id", 
               onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("Fk_Customers_AspNetUsers_CustomserID", "Customers");

        }
    }
}
