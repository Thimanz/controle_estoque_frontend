using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.DTO
{
    public class LocalItemDTO
    {
        public LocalItemDTO(Guid localId, Guid produtoId, string? nomeProduto, decimal comprimento, decimal largura,
            decimal altura, decimal precoUnitario, int quantidade, Guid? pedidoCompraId, Guid? pedidoVendaId)
        {
            LocalId = localId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            PedidoCompraId = pedidoCompraId;
            PedidoVendaId = pedidoVendaId;
        }

        public Guid LocalId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
    }
}
