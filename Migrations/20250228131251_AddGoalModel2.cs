using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Migrations
{
    public partial class AddGoalModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetCalories",
                table: "Goal");

            migrationBuilder.AddColumn<double>(
                name: "StartWeight",
                table: "Goal",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetWeight",
                table: "Goal",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartWeight",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "Goal");

            migrationBuilder.AddColumn<int>(
                name: "TargetCalories",
                table: "Goal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
