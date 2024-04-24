using GDE.Estoque.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.Infra.Data.Mappings
{
    public class LocalItemMapping : IEntityTypeConfiguration<LocalItem>
    {
        public void Configure(EntityTypeBuilder<LocalItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Dimensoes, tf =>
            {
                tf.Property(c => c.Comprimento)
                    .HasColumnName("Comprimento");
                tf.Property(c => c.Altura)
                    .HasColumnName("Altura");
                tf.Property(c => c.Largura)
                    .HasColumnName("Largura");
            });

            builder.HasOne(c => c.Local)
                .WithMany(c => c.LocalItens);
        }
    }
}