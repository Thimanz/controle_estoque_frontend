namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class ProdutoDTO
    {
        public string? Nome { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? Imagem { get; set; }
    }
}
