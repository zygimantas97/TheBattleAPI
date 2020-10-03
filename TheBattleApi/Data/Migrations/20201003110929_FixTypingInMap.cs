using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class FixTypingInMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmenyShot_Y",
                table: "Maps");

            migrationBuilder.AddColumn<int>(
                name: "EnemyShot_Y",
                table: "Maps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnemyShot_Y",
                table: "Maps");

            migrationBuilder.AddColumn<int>(
                name: "EmenyShot_Y",
                table: "Maps",
                type: "int",
                nullable: true);
        }
    }
}
