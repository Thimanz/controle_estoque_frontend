using GDE.Produtos.API.Models;

namespace GDE.Produtos.API.ViewModels
{
    public class AdicionarProdutoViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? CodigoBarras { get; set; }
        public string? NomeCategoria { get; set; }
        public string? DescricaoCategoria { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? Imagem { get; set; }
        public int NivelMinimoEstoque { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
    }
}
