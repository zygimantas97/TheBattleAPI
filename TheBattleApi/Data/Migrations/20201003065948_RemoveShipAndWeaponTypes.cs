using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class RemoveShipAndWeaponTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeaponTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeaponTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeaponTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WeaponTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WeaponTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "IsCOmpleted",
                table: "Maps",
                newName: "IsCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Maps",
                newName: "IsCOmpleted");

            migrationBuilder.InsertData(
                table: "ShipTypes",
                columns: new[] { "Id", "Name", "Size" },
                values: new object[,]
                {
                    { 1, "x1", 1 },
                    { 2, "x2", 2 },
                    { 3, "x3", 3 },
                    { 4, "x4", 4 }
                });

            migrationBuilder.InsertData(
                table: "WeaponTypes",
                columns: new[] { "Id", "IsMine", "Name", "Power" },
                values: new object[,]
                {
                    { 1, true, "Mine", 1 },
                    { 2, false, "Bullet", 1 },
                    { 3, false, "Bomb", 1 },
                    { 4, false, "Torpedo", 1 },
                    { 5, false, "Missile", 1 }
                });
        }
    }
}
