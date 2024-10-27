using GDE.Produtos.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GDE.Produtos.API.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .HasColumnType("varchar(2500)");

            builder.OwnsOne(c => c.Dimensoes, tf =>
            {
                tf.Property(c => c.Comprimento)
                    .HasColumnName("Comprimento");
                tf.Property(c => c.Altura)
                    .HasColumnName("Altura");
                tf.Property(c => c.Largura)
                    .HasColumnName("Largura");

            });

            builder.HasOne(c => c.Categoria)
                .WithMany(c => c.Produtos);

            builder.ToTable("Produtos");
        }
    }
}
