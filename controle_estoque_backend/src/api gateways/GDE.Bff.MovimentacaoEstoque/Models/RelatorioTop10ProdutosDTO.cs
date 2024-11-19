namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class RelatorioTop10ProdutosDTO
    {
        public Guid ProdutoId { get; set; }
        public string? Produto { get; set; }
        public int QuantidadeVendida { get; set; }
    }
}
