using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat2.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UmetnikID",
                table: "UmetnickaDela",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UmetnickaDela_UmetnikID",
                table: "UmetnickaDela",
                column: "UmetnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_UmetnickaDela_Umetnici_UmetnikID",
                table: "UmetnickaDela",
                column: "UmetnikID",
                principalTable: "Umetnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UmetnickaDela_Umetnici_UmetnikID",
                table: "UmetnickaDela");

            migrationBuilder.DropIndex(
                name: "IX_UmetnickaDela_UmetnikID",
                table: "UmetnickaDela");

            migrationBuilder.DropColumn(
                name: "UmetnikID",
                table: "UmetnickaDela");
        }
    }
}
