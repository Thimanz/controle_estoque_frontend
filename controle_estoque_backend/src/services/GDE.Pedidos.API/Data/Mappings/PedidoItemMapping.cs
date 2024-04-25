using GDE.Pedidos.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Pedidos.API.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.PedidoCompra)
                .WithMany(c => c.PedidoItens);

            builder.HasOne(c => c.PedidoVenda)
                .WithMany(c => c.PedidoItens);
        }
    }
}
