using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileService.Infrastructure.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class changeMealPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Profiles_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                schema: "UserProfileServiceSchema",
                table: "DayResults");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "UserProfileServiceSchema",
                table: "DayResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ActivityLevel",
                schema: "UserProfileServiceSchema",
                table: "DayResults",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                schema: "UserProfileServiceSchema",
                table: "DayResults",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Profiles_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food",
                column: "ProfileId",
                principalSchema: "UserProfileServiceSchema",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
