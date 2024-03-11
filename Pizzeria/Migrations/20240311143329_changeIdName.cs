using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class changeIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProdottiAcquistati",
                newName: "IdProdottoAcquistato");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prodotti",
                newName: "IdProdotto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProdottoAcquistato",
                table: "ProdottiAcquistati",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdProdotto",
                table: "Prodotti",
                newName: "Id");
        }
    }
}
