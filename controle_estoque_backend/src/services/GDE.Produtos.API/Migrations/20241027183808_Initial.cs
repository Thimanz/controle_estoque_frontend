﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GDE.Produtos.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodigoBarras = table.Column<string>(type: "varchar(100)", nullable: true),
                    PrecoCusto = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "TEXT", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(2500)", nullable: true),
                    QuantidadeEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelMinimoEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comprimento = table.Column<decimal>(type: "TEXT", nullable: true),
                    Largura = table.Column<decimal>(type: "TEXT", nullable: true),
                    Altura = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}