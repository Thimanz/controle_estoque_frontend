using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class ObterPedidoCompraDTO
    {
        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public string? NomeFornecedor { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; private set; } = new List<BuscarPedidoItemDTO>();

        public static ObterPedidoCompraDTO FromDomain(PedidoCompra pedidoCompra) =>
            new ObterPedidoCompraDTO
            {
                Id = pedidoCompra.Id,
                IdFuncionarioResponsavel = pedidoCompra.IdFuncionarioResponsavel,
                NomeFornecedor = pedidoCompra.NomeFornecedor,
                Numero = pedidoCompra.Numero,
                PrecoTotal = pedidoCompra.PrecoTotal,
                DataCriacao = pedidoCompra.DataCriacao,
                PedidoItens = pedidoCompra.PedidoItens.Select(BuscarPedidoItemDTO.FromDomain).ToList(),
            };
    }
}
