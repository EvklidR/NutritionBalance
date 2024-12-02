using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanService.Infrastructure.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                schema: "MealPlanServiceSchema",
                table: "MealPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "MealPlanServiceSchema",
                table: "MealPlans");
        }
    }
}
