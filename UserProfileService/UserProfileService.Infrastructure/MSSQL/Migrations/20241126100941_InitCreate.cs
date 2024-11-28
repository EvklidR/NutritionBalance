using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileService.Infrastructure.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserProfileServiceSchema");

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ActivityLevel = table.Column<double>(type: "float", nullable: false),
                    DesiredGlassesOfWater = table.Column<int>(type: "int", nullable: false),
                    MealPlanId = table.Column<int>(type: "int", nullable: true),
                    DateOfStartPlan = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayResults",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    ActivityLevel = table.Column<double>(type: "float", nullable: true),
                    GlassesOfWater = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayResults_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    FoodType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightOfPortion = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_DayResults_DayId",
                        column: x => x.DayId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "DayResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientOfDishes",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientOfDishes", x => new { x.DishId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientOfDishes_Food_DishId",
                        column: x => x.DishId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientOfDishes_Food_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EatenFoods",
                schema: "UserProfileServiceSchema",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EatenFoods", x => new { x.FoodId, x.MealId });
                    table.ForeignKey(
                        name: "FK_EatenFoods_Food_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EatenFoods_Meals_MealId",
                        column: x => x.MealId,
                        principalSchema: "UserProfileServiceSchema",
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayResults_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "DayResults",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EatenFoods_MealId",
                schema: "UserProfileServiceSchema",
                table: "EatenFoods",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProfileId_Name",
                schema: "UserProfileServiceSchema",
                table: "Food",
                columns: new[] { "ProfileId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientOfDishes_IngredientId",
                schema: "UserProfileServiceSchema",
                table: "IngredientOfDishes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DayId",
                schema: "UserProfileServiceSchema",
                table: "Meals",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId_Name",
                schema: "UserProfileServiceSchema",
                table: "Profiles",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EatenFoods",
                schema: "UserProfileServiceSchema");

            migrationBuilder.DropTable(
                name: "IngredientOfDishes",
                schema: "UserProfileServiceSchema");

            migrationBuilder.DropTable(
                name: "Meals",
                schema: "UserProfileServiceSchema");

            migrationBuilder.DropTable(
                name: "Food",
                schema: "UserProfileServiceSchema");

            migrationBuilder.DropTable(
                name: "DayResults",
                schema: "UserProfileServiceSchema");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "UserProfileServiceSchema");
        }
    }
}
