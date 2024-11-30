using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Estoque.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Endereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Locais",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Locais");
        }
    }
}
