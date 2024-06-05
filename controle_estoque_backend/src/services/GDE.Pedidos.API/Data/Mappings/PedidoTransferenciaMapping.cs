using GDE.Pedidos.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Pedidos.API.Data.Mappings
{
    public class PedidoTransferenciaMapping : IEntityTypeConfiguration<PedidoTransferencia>
    {
        public void Configure(EntityTypeBuilder<PedidoTransferencia> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Numero)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.PedidoItens)
                .WithOne(c => c.PedidoTransferencia)
                .HasForeignKey(c => c.PedidoTransferenciaId);
        }
    }
}
