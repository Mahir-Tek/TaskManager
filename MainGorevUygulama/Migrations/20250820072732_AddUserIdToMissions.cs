using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainGorevUygulama.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToMissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Missions");
        }
    }
}
