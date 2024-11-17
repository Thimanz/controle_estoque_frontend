using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Estoque.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", nullable: true),
                    Comprimento = table.Column<decimal>(type: "numeric", nullable: false),
                    Largura = table.Column<decimal>(type: "numeric", nullable: false),
                    Altura = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocalId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Comprimento = table.Column<decimal>(type: "numeric", nullable: false),
                    Largura = table.Column<decimal>(type: "numeric", nullable: false),
                    Altura = table.Column<decimal>(type: "numeric", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalItens_Locais_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalItens_LocalId",
                table: "LocalItens",
                column: "LocalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalItens");

            migrationBuilder.DropTable(
                name: "Locais");
        }
    }
}
