using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Migrations
{
    public partial class DatabaseNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diets_FoodCategories_CategoryId",
                table: "Diets");

            migrationBuilder.DropIndex(
                name: "IX_Diets_CategoryId",
                table: "Diets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Diets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Diets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diets_CategoryId",
                table: "Diets",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diets_FoodCategories_CategoryId",
                table: "Diets",
                column: "CategoryId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
