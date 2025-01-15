using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerTracker.Data.Migrations
{
    public partial class NewDiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Diet");

            migrationBuilder.AlterColumn<double>(
                name: "Name",
                table: "Foods",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Diet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Diet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diet_CategoryId",
                table: "Diet",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_FoodId",
                table: "Diet",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diet_FoodCategories_CategoryId",
                table: "Diet",
                column: "CategoryId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diet_Foods_FoodId",
                table: "Diet",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diet_FoodCategories_CategoryId",
                table: "Diet");

            migrationBuilder.DropForeignKey(
                name: "FK_Diet_Foods_FoodId",
                table: "Diet");

            migrationBuilder.DropIndex(
                name: "IX_Diet_CategoryId",
                table: "Diet");

            migrationBuilder.DropIndex(
                name: "IX_Diet_FoodId",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Diet");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Diet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
