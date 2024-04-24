using GDE.Produtos.API.Entities;

namespace GDE.Produtos.API.Models.ViewModels
{
    public class ProdutoViewModel
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? CodigoBarras { get; set; }
        public CategoriaViewModel? Categoria { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public byte[]? Imagem { get; set; }
        public int NivelMinimoEstoque { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }

        public static ProdutoViewModel FromEntity(Produto produto) =>
            new ProdutoViewModel()
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                CodigoBarras = produto.CodigoBarras,
                Categoria = CategoriaViewModel.FromEntity(produto.Categoria),
                PrecoCusto = produto.PrecoCusto,
                PrecoVenda = produto.PrecoVenda,
                NivelMinimoEstoque = produto.NivelMinimoEstoque,
                Comprimento = produto.Dimensoes.Comprimento,
                Largura = produto.Dimensoes.Largura,
                Altura = produto.Dimensoes.Altura,
                //Peso = produto.Dimensoes.Peso
            };

    }
}
