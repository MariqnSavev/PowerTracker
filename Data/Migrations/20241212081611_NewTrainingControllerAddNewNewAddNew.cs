using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class NewTrainingControllerAddNewNewAddNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealDescription",
                table: "Diet");

            migrationBuilder.AlterColumn<double>(
                name: "Calories",
                table: "Diet",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "FoodName",
                table: "Diet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "QuantityInGrams",
                table: "Diet",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodName",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "QuantityInGrams",
                table: "Diet");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Diet",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "MealDescription",
                table: "Diet",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
