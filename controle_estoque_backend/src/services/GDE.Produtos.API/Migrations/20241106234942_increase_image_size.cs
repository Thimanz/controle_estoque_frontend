using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Produtos.API.Migrations
{
    /// <inheritdoc />
    public partial class increase_image_size : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Produtos",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2500)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Imagem",
                table: "Produtos",
                type: "varchar(2500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);
        }
    }
}
