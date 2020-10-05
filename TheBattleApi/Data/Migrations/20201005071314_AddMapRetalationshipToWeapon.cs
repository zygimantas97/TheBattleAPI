using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class AddMapRetalationshipToWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Maps_MapUserId_MapRoomId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_MapUserId_MapRoomId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "MapRoomId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "MapUserId",
                table: "Weapons");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Weapons",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_UserId_RoomId",
                table: "Weapons",
                columns: new[] { "UserId", "RoomId" });

            migrationBuilder.AddForeignKey(
                name: "fkc_weapon_map",
                table: "Weapons",
                columns: new[] { "UserId", "RoomId" },
                principalTable: "Maps",
                principalColumns: new[] { "UserId", "RoomId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fkc_weapon_map",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_UserId_RoomId",
                table: "Weapons");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapRoomId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapUserId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_MapUserId_MapRoomId",
                table: "Weapons",
                columns: new[] { "MapUserId", "MapRoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Maps_MapUserId_MapRoomId",
                table: "Weapons",
                columns: new[] { "MapUserId", "MapRoomId" },
                principalTable: "Maps",
                principalColumns: new[] { "UserId", "RoomId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
