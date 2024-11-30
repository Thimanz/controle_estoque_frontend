﻿// <auto-generated />
using System;
using GDE.Pedidos.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GDE.Pedidos.API.Migrations
{
    [DbContext(typeof(PedidosContext))]
    [Migration("20240508200729_PedidoTransferencia")]
    partial class PedidoTransferencia
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoCompra", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFuncionarioResponsavel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeFornecedor")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("PedidosCompra");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PedidoCompraId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PedidoTransferenciaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PedidoVendaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoCompraId");

                    b.HasIndex("PedidoTransferenciaId");

                    b.HasIndex("PedidoVendaId");

                    b.ToTable("PedidoItens");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoTransferencia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFuncionarioResponsavel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("PedidosTransferencia");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoVenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFuncionarioResponsavel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeCliente")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("PedidosVenda");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoItem", b =>
                {
                    b.HasOne("GDE.Pedidos.API.Models.PedidoCompra", "PedidoCompra")
                        .WithMany("PedidoItens")
                        .HasForeignKey("PedidoCompraId");

                    b.HasOne("GDE.Pedidos.API.Models.PedidoTransferencia", "PedidoTransferencia")
                        .WithMany("PedidoItens")
                        .HasForeignKey("PedidoTransferenciaId");

                    b.HasOne("GDE.Pedidos.API.Models.PedidoVenda", "PedidoVenda")
                        .WithMany("PedidoItens")
                        .HasForeignKey("PedidoVendaId");

                    b.Navigation("PedidoCompra");

                    b.Navigation("PedidoTransferencia");

                    b.Navigation("PedidoVenda");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoCompra", b =>
                {
                    b.Navigation("PedidoItens");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoTransferencia", b =>
                {
                    b.Navigation("PedidoItens");
                });

            modelBuilder.Entity("GDE.Pedidos.API.Models.PedidoVenda", b =>
                {
                    b.Navigation("PedidoItens");
                });
#pragma warning restore 612, 618
        }
    }
}
