using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class PedidoTransferencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PedidoTransferenciaId",
                table: "PedidoItens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PedidosTransferencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFuncionarioResponsavel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosTransferencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoTransferenciaId",
                table: "PedidoItens",
                column: "PedidoTransferenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_PedidosTransferencia_PedidoTransferenciaId",
                table: "PedidoItens",
                column: "PedidoTransferenciaId",
                principalTable: "PedidosTransferencia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_PedidosTransferencia_PedidoTransferenciaId",
                table: "PedidoItens");

            migrationBuilder.DropTable(
                name: "PedidosTransferencia");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_PedidoTransferenciaId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "PedidoTransferenciaId",
                table: "PedidoItens");
        }
    }
}
