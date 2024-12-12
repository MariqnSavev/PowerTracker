using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class NewTrainingControllerAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CaloriesBurned",
                table: "Training",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Training",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "WeightInKg",
                table: "Training",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "WeightInKg",
                table: "Training");

            migrationBuilder.AlterColumn<int>(
                name: "CaloriesBurned",
                table: "Training",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
