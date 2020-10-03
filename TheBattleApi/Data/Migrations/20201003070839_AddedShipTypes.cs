using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class AddedShipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShipTypes",
                columns: new[] { "Id", "IsSubmarine", "Name", "Size" },
                values: new object[,]
                {
                    { 1, false, "Small Destroyer", 1 },
                    { 2, false, "Medium Destroyer", 2 },
                    { 3, false, "Large Destroyer", 3 },
                    { 4, false, "Atomic Destroyer", 4 },
                    { 5, true, "Small Submarine", 1 },
                    { 6, true, "Medium Submarine", 2 },
                    { 7, true, "Large Submarine", 3 },
                    { 8, true, "Atomic Submarine", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ShipTypes",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
