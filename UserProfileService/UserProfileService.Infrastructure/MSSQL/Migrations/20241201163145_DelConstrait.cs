using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileService.Infrastructure.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class DelConstrait : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Food_ProfileId_Name",
                schema: "UserProfileServiceSchema",
                table: "Food");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "UserProfileServiceSchema",
                table: "Food",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Food_ProfileId",
                schema: "UserProfileServiceSchema",
                table: "Food");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "UserProfileServiceSchema",
                table: "Food",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Food_ProfileId_Name",
                schema: "UserProfileServiceSchema",
                table: "Food",
                columns: new[] { "ProfileId", "Name" },
                unique: true);
        }
    }
}
