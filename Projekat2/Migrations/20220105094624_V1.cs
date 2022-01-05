using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat2.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galerije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galerije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Umetnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UmetnickoIme = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DrzavaRodjenja = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Umetnici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Izlozbe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivIzlozbe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumKraja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GalerijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izlozbe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Izlozbe_Galerije_GalerijaID",
                        column: x => x.GalerijaID,
                        principalTable: "Galerije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UmetnickaDela",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Godina = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    TipDela = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    GalerijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UmetnickaDela", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UmetnickaDela_Galerije_GalerijaID",
                        column: x => x.GalerijaID,
                        principalTable: "Galerije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GalerijaUmetnici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmetnikID = table.Column<int>(type: "int", nullable: true),
                    GalerijaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalerijaUmetnici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GalerijaUmetnici_Galerije_GalerijaID",
                        column: x => x.GalerijaID,
                        principalTable: "Galerije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GalerijaUmetnici_Umetnici_UmetnikID",
                        column: x => x.UmetnikID,
                        principalTable: "Umetnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePosetioca = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrezimePosetioca = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IzlozbaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_Izlozbe_IzlozbaID",
                        column: x => x.IzlozbaID,
                        principalTable: "Izlozbe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DelaIzlozbe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzlozbaID = table.Column<int>(type: "int", nullable: true),
                    UmetnickoDeloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelaIzlozbe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DelaIzlozbe_Izlozbe_IzlozbaID",
                        column: x => x.IzlozbaID,
                        principalTable: "Izlozbe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DelaIzlozbe_UmetnickaDela_UmetnickoDeloID",
                        column: x => x.UmetnickoDeloID,
                        principalTable: "UmetnickaDela",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelaIzlozbe_IzlozbaID",
                table: "DelaIzlozbe",
                column: "IzlozbaID");

            migrationBuilder.CreateIndex(
                name: "IX_DelaIzlozbe_UmetnickoDeloID",
                table: "DelaIzlozbe",
                column: "UmetnickoDeloID");

            migrationBuilder.CreateIndex(
                name: "IX_GalerijaUmetnici_GalerijaID",
                table: "GalerijaUmetnici",
                column: "GalerijaID");

            migrationBuilder.CreateIndex(
                name: "IX_GalerijaUmetnici_UmetnikID",
                table: "GalerijaUmetnici",
                column: "UmetnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Izlozbe_GalerijaID",
                table: "Izlozbe",
                column: "GalerijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_IzlozbaID",
                table: "Karte",
                column: "IzlozbaID");

            migrationBuilder.CreateIndex(
                name: "IX_UmetnickaDela_GalerijaID",
                table: "UmetnickaDela",
                column: "GalerijaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelaIzlozbe");

            migrationBuilder.DropTable(
                name: "GalerijaUmetnici");

            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "UmetnickaDela");

            migrationBuilder.DropTable(
                name: "Umetnici");

            migrationBuilder.DropTable(
                name: "Izlozbe");

            migrationBuilder.DropTable(
                name: "Galerije");
        }
    }
}
