using GDE.Produtos.API.Entities;

namespace GDE.Produtos.API.Models.InputModels
{
    public class ProdutoInputModel
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? CodigoBarras { get; set; }
        public Guid CategoriaId { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? Imagem { get; set; }
        public int NivelMinimoEstoque { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }

        public Produto ToEntity() =>
            new Produto(Nome, Descricao, CodigoBarras, CategoriaId, PrecoCusto, PrecoVenda, 
                     Imagem, NivelMinimoEstoque, Comprimento, Largura, Altura, Peso);
    }
}
