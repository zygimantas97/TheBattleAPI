using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class RemovedWeaponGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_weapon_group",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "WeaponGroups");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_UserId_RoomId_WeaponTypeId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "IsHorizontal",
                table: "Ships");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Weapons",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapRoomId",
                table: "Weapons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapUserId",
                table: "Weapons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "XOffset",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YOffset",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmenyShot_Y",
                table: "Maps",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnemyShot_X",
                table: "Maps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_WeaponTypeId",
                table: "Weapons",
                column: "WeaponTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_MapUserId_MapRoomId",
                table: "Weapons",
                columns: new[] { "MapUserId", "MapRoomId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_WeaponTypes_WeaponTypeId",
                table: "Weapons",
                column: "WeaponTypeId",
                principalTable: "WeaponTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Maps_MapUserId_MapRoomId",
                table: "Weapons",
                columns: new[] { "MapUserId", "MapRoomId" },
                principalTable: "Maps",
                principalColumns: new[] { "UserId", "RoomId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_WeaponTypes_WeaponTypeId",
                table: "Weapons");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Maps_MapUserId_MapRoomId",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_WeaponTypeId",
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

            migrationBuilder.DropColumn(
                name: "XOffset",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "YOffset",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "EmenyShot_Y",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "EnemyShot_X",
                table: "Maps");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomId",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHorizontal",
                table: "Ships",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "WeaponGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WeaponTypeId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_weapon_group", x => new { x.UserId, x.RoomId, x.WeaponTypeId });
                    table.ForeignKey(
                        name: "FK_WeaponGroups_WeaponTypes_WeaponTypeId",
                        column: x => x.WeaponTypeId,
                        principalTable: "WeaponTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeaponGroups_Maps_UserId_RoomId",
                        columns: x => new { x.UserId, x.RoomId },
                        principalTable: "Maps",
                        principalColumns: new[] { "UserId", "RoomId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_UserId_RoomId_WeaponTypeId",
                table: "Weapons",
                columns: new[] { "UserId", "RoomId", "WeaponTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponGroups_WeaponTypeId",
                table: "WeaponGroups",
                column: "WeaponTypeId");

            migrationBuilder.AddForeignKey(
                name: "fk_weapon_group",
                table: "Weapons",
                columns: new[] { "UserId", "RoomId", "WeaponTypeId" },
                principalTable: "WeaponGroups",
                principalColumns: new[] { "UserId", "RoomId", "WeaponTypeId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
