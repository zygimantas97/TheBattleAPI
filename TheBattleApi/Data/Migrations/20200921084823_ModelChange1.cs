using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class ModelChange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMissile",
                table: "WeaponTypes");

            migrationBuilder.DropColumn(
                name: "OffsetX",
                table: "ShipTypes");

            migrationBuilder.DropColumn(
                name: "OffsetY",
                table: "ShipTypes");

            migrationBuilder.AddColumn<bool>(
                name: "IsMine",
                table: "WeaponTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "ShipTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsHorizontal",
                table: "Ships",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMine",
                table: "WeaponTypes");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShipTypes");

            migrationBuilder.DropColumn(
                name: "IsHorizontal",
                table: "Ships");

            migrationBuilder.AddColumn<bool>(
                name: "IsMissile",
                table: "WeaponTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OffsetX",
                table: "ShipTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OffsetY",
                table: "ShipTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
