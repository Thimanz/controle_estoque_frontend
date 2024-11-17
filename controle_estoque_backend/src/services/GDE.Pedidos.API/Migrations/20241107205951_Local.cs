using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    /// <inheritdoc />
    public partial class Local : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeLocalDestino",
                table: "PedidosTransferencia",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNome",
                table: "PedidoItens",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeLocalDestino",
                table: "PedidosTransferencia");

            migrationBuilder.DropColumn(
                name: "LocalNome",
                table: "PedidoItens");
        }
    }
}
