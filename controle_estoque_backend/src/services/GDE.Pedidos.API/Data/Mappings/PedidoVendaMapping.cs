﻿using GDE.Pedidos.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Pedidos.API.Data.Mappings
{
    public class PedidoVendaMapping : IEntityTypeConfiguration<PedidoVenda>
    {
        public void Configure(EntityTypeBuilder<PedidoVenda> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.PedidoItens)
                .WithOne(c => c.PedidoVenda)
                .HasForeignKey(c => c.PedidoVendaId);
        }
    }
}
