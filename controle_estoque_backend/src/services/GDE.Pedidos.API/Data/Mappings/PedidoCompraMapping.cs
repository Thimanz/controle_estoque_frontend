using GDE.Pedidos.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Pedidos.API.Data.Mappings
{
    public class PedidoCompraMapping : IEntityTypeConfiguration<PedidoCompra>
    {
        public void Configure(EntityTypeBuilder<PedidoCompra> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.PedidoItens)
                .WithOne(c => c.PedidoCompra)
                .HasForeignKey(c => c.PedidoCompraId);
        }
    }
}
