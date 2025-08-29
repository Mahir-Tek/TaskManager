using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainGorevUygulama.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Missions_UserId",
                table: "Missions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_UserId",
                table: "Missions");
        }
    }
}
