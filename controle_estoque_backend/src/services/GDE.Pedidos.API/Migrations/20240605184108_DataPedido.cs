using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class DataPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PedidosVenda",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PedidosTransferencia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PedidosCompra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PedidosVenda");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PedidosTransferencia");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PedidosCompra");
        }
    }
}
