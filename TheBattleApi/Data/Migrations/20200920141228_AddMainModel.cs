using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class AddMainModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    IsHostTurn = table.Column<bool>(nullable: false),
                    HostUserId = table.Column<string>(nullable: true),
                    GuestUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_AspNetUsers_GuestUserId",
                        column: x => x.GuestUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_AspNetUsers_HostUserId",
                        column: x => x.HostUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    OffsetX = table.Column<int>(nullable: false),
                    OffsetY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    IsMissile = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoomId = table.Column<string>(nullable: false),
                    IsCOmpleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map", x => new { x.UserId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_Maps_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoomId = table.Column<string>(nullable: false),
                    ShipTypeId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ship_group", x => new { x.UserId, x.RoomId, x.ShipTypeId });
                    table.ForeignKey(
                        name: "FK_ShipGroups_ShipTypes_ShipTypeId",
                        column: x => x.ShipTypeId,
                        principalTable: "ShipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipGroups_Maps_UserId_RoomId",
                        columns: x => new { x.UserId, x.RoomId },
                        principalTable: "Maps",
                        principalColumns: new[] { "UserId", "RoomId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoomId = table.Column<string>(nullable: false),
                    WeaponTypeId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    HP = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoomId = table.Column<string>(nullable: true),
                    ShipTypeId = table.Column<int>(nullable: false),
                    ShipGroupUserId = table.Column<string>(nullable: true),
                    ShipGroupRoomId = table.Column<string>(nullable: true),
                    ShipGroupShipTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_ShipGroups_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId",
                        columns: x => new { x.ShipGroupUserId, x.ShipGroupRoomId, x.ShipGroupShipTypeId },
                        principalTable: "ShipGroups",
                        principalColumns: new[] { "UserId", "RoomId", "ShipTypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoomId = table.Column<string>(nullable: true),
                    WeaponTypeId = table.Column<int>(nullable: false),
                    WeaponGroupUserId = table.Column<string>(nullable: true),
                    WeaponGroupRoomId = table.Column<string>(nullable: true),
                    WeaponGroupWeaponTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_WeaponGroups_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId",
                        columns: x => new { x.WeaponGroupUserId, x.WeaponGroupRoomId, x.WeaponGroupWeaponTypeId },
                        principalTable: "WeaponGroups",
                        principalColumns: new[] { "UserId", "RoomId", "WeaponTypeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maps_RoomId",
                table: "Maps",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GuestUserId",
                table: "Rooms",
                column: "GuestUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostUserId",
                table: "Rooms",
                column: "HostUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipGroups_ShipTypeId",
                table: "ShipGroups",
                column: "ShipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId",
                table: "Ships",
                columns: new[] { "ShipGroupUserId", "ShipGroupRoomId", "ShipGroupShipTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponGroups_WeaponTypeId",
                table: "WeaponGroups",
                column: "WeaponTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId",
                table: "Weapons",
                columns: new[] { "WeaponGroupUserId", "WeaponGroupRoomId", "WeaponGroupWeaponTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "ShipGroups");

            migrationBuilder.DropTable(
                name: "WeaponGroups");

            migrationBuilder.DropTable(
                name: "ShipTypes");

            migrationBuilder.DropTable(
                name: "WeaponTypes");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
