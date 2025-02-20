using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class FixColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodCategories_NameOfcategorieId",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "NameOfcategorieId",
                table: "Foods",
                newName: "NameOfCategorieId");

            migrationBuilder.RenameColumn(
                name: "IdCategorie",
                table: "Foods",
                newName: "FoodCategorieID");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_NameOfcategorieId",
                table: "Foods",
                newName: "IX_Foods_NameOfCategorieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "CaloriesPer100g",
                table: "Foods",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodCategories_NameOfCategorieId",
                table: "Foods",
                column: "NameOfCategorieId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodCategories_NameOfCategorieId",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "NameOfCategorieId",
                table: "Foods",
                newName: "NameOfcategorieId");

            migrationBuilder.RenameColumn(
                name: "FoodCategorieID",
                table: "Foods",
                newName: "IdCategorie");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_NameOfCategorieId",
                table: "Foods",
                newName: "IX_Foods_NameOfcategorieId");

            migrationBuilder.AlterColumn<double>(
                name: "Name",
                table: "Foods",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CaloriesPer100g",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodCategories_NameOfcategorieId",
                table: "Foods",
                column: "NameOfcategorieId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
