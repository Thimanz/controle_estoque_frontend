using GDE.Core.Domain_Objects;

namespace GDE.Estoque.API.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
        public string? CodigoBarras { get; set; }
        public Categoria? Categoria { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? Imagem { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int NivelMinimoEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
