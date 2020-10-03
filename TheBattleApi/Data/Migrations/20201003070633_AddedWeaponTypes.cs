using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class AddedWeaponTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmarine",
                table: "ShipTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "WeaponTypes",
                columns: new[] { "Id", "IsMine", "Name", "Power" },
                values: new object[] { 1, false, "Bomb", 1 });

            migrationBuilder.InsertData(
                table: "WeaponTypes",
                columns: new[] { "Id", "IsMine", "Name", "Power" },
                values: new object[] { 2, false, "Torpedo", 1 });

            migrationBuilder.InsertData(
                table: "WeaponTypes",
                columns: new[] { "Id", "IsMine", "Name", "Power" },
                values: new object[] { 3, true, "Mine", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "IsSubmarine",
                table: "ShipTypes");
        }
    }
}
