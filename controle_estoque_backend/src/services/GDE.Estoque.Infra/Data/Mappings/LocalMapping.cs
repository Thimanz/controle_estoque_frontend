using GDE.Estoque.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Estoque.Infra.Data.Mappings
{
    public class LocalMapping : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");
            
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(250)");

            builder.OwnsOne(c => c.Dimensoes, tf =>
            {
                tf.Property(c => c.Comprimento)
                    .HasColumnName("Comprimento");
                tf.Property(c => c.Altura)
                    .HasColumnName("Altura");
                tf.Property(c => c.Largura)
                    .HasColumnName("Largura");
            });

            builder.HasMany(c => c.LocalItens)
                .WithOne(c => c.Local)
                .HasForeignKey(c => c.LocalId);
        }
    }
}
