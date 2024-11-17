namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class BuscarPedidoItemDTO
    {
        public Guid ProdutoId { get; set; }
        public LocalPedidoDTO? Local { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string? Imagem { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid? PedidoTransferenciaId { get; set; }
        public string? DataValidade { get; set; }
    }
}