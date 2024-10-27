using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidosCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeFornecedor = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdFuncionarioResponsavel = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidosTransferencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdLocalDestino = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdFuncionarioResponsavel = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosTransferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidosVenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeCliente = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdFuncionarioResponsavel = table.Column<Guid>(type: "TEXT", nullable: false),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosVenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LocalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    PedidoCompraId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PedidoVendaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PedidoTransferenciaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DataValidade = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItens_PedidosCompra_PedidoCompraId",
                        column: x => x.PedidoCompraId,
                        principalTable: "PedidosCompra",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoItens_PedidosTransferencia_PedidoTransferenciaId",
                        column: x => x.PedidoTransferenciaId,
                        principalTable: "PedidosTransferencia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoItens_PedidosVenda_PedidoVendaId",
                        column: x => x.PedidoVendaId,
                        principalTable: "PedidosVenda",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoCompraId",
                table: "PedidoItens",
                column: "PedidoCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoTransferenciaId",
                table: "PedidoItens",
                column: "PedidoTransferenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoVendaId",
                table: "PedidoItens",
                column: "PedidoVendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropTable(
                name: "PedidosCompra");

            migrationBuilder.DropTable(
                name: "PedidosTransferencia");

            migrationBuilder.DropTable(
                name: "PedidosVenda");
        }
    }
}
