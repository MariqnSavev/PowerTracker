using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class AddDietTableAddAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Diet");

            migrationBuilder.RenameColumn(
                name: "FoodName",
                table: "Diet",
                newName: "Name");

            migrationBuilder.AddColumn<double>(
                name: "CaloriesPer100g",
                table: "Diet",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesPer100g",
                table: "Diet");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Diet",
                newName: "FoodName");

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Diet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaloriesPer100g = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });
        }
    }
}
