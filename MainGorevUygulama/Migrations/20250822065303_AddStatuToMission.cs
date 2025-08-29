using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainGorevUygulama.Migrations
{
    /// <inheritdoc />
    public partial class AddStatuToMission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Missions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Statu",
                table: "Missions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Statu",
                table: "Missions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
