using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class NumeroPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "PedidosVenda",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "PedidosTransferencia",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "PedidosCompra",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "PedidosVenda");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "PedidosTransferencia");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "PedidosCompra");
        }
    }
}
