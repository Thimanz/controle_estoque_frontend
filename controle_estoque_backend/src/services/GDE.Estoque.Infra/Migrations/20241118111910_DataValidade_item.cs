using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Estoque.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DataValidade_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataValidade",
                table: "LocalItens",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataValidade",
                table: "LocalItens");
        }
    }
}
