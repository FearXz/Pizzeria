using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class firstTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoProdotto = table.Column<double>(type: "float", nullable: false),
                    TempoConsegna = table.Column<int>(type: "int", nullable: false),
                    Ingredienti = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    IdUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.IdUtente);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    IdOrdine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IndirizzoDiConsegna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataOrdine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEvaso = table.Column<bool>(type: "bit", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrezzoTotale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.IdOrdine);
                    table.ForeignKey(
                        name: "FK_Ordini_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdottiAcquistati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrdine = table.Column<int>(type: "int", nullable: false),
                    IdProdotto = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiAcquistati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdottiAcquistati_Ordini_IdOrdine",
                        column: x => x.IdOrdine,
                        principalTable: "Ordini",
                        principalColumn: "IdOrdine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdottiAcquistati_Prodotti_IdProdotto",
                        column: x => x.IdProdotto,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_IdUtente",
                table: "Ordini",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiAcquistati_IdOrdine",
                table: "ProdottiAcquistati",
                column: "IdOrdine");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiAcquistati_IdProdotto",
                table: "ProdottiAcquistati",
                column: "IdProdotto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdottiAcquistati");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Prodotti");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
