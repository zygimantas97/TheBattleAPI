using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBattleApi.Data.Migrations
{
    public partial class AddedWinnerProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WinnerId",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_WinnerId",
                table: "Rooms",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetUsers_WinnerId",
                table: "Rooms",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetUsers_WinnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_WinnerId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Rooms");
        }
    }
}
