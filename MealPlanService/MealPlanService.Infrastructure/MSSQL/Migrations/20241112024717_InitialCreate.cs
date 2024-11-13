using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanService.Infrastructure.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MealPlanServiceSchema");

            migrationBuilder.CreateTable(
                name: "MealPlans",
                schema: "MealPlanServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanDays",
                schema: "MealPlanServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealPlanId = table.Column<int>(type: "int", nullable: false),
                    NumberOfDay = table.Column<int>(type: "int", nullable: false),
                    CaloriePercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPlanDays_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalSchema: "MealPlanServiceSchema",
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutrientsOfDay",
                schema: "MealPlanServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealPlanDayId = table.Column<int>(type: "int", nullable: false),
                    NutrientType = table.Column<int>(type: "int", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutrientsOfDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutrientsOfDay_MealPlanDays_MealPlanDayId",
                        column: x => x.MealPlanDayId,
                        principalSchema: "MealPlanServiceSchema",
                        principalTable: "MealPlanDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanDays_MealPlanId_NumberOfDay",
                schema: "MealPlanServiceSchema",
                table: "MealPlanDays",
                columns: new[] { "MealPlanId", "NumberOfDay" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NutrientsOfDay_MealPlanDayId_NutrientType",
                schema: "MealPlanServiceSchema",
                table: "NutrientsOfDay",
                columns: new[] { "MealPlanDayId", "NutrientType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutrientsOfDay",
                schema: "MealPlanServiceSchema");

            migrationBuilder.DropTable(
                name: "MealPlanDays",
                schema: "MealPlanServiceSchema");

            migrationBuilder.DropTable(
                name: "MealPlans",
                schema: "MealPlanServiceSchema");
        }
    }
}
