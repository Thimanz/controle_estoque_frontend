namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class PedidoItemDTO
    {
        //public Guid? PedidoCompraId { get; set; }
        //public Guid? PedidoVendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public string? NomeProduto { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime DataValidade { get; set; }
    }
}