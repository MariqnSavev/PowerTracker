using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class DietMigrationUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diet_FoodCategories_CategoryId",
                table: "Diet");

            migrationBuilder.DropIndex(
                name: "IX_Diet_CategoryId",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Diet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Diet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diet_CategoryId",
                table: "Diet",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diet_FoodCategories_CategoryId",
                table: "Diet",
                column: "CategoryId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
