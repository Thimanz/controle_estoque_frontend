using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class ObterPedidoVendaDTO
    {
        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public string? NomeCliente { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public string? DataCriacao { get; private set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; private set; } = new List<BuscarPedidoItemDTO>();

        public static ObterPedidoVendaDTO FromDomain(PedidoVenda pedidoVenda) =>
            new ObterPedidoVendaDTO
            {
                Id = pedidoVenda.Id,
                IdFuncionarioResponsavel = pedidoVenda.IdFuncionarioResponsavel,
                NomeCliente = pedidoVenda.NomeCliente,
                Numero = pedidoVenda.Numero,
                PrecoTotal = pedidoVenda.PrecoTotal,
                DataCriacao = pedidoVenda.DataCriacao.ToString("dd/MM/yyyy"),
                PedidoItens = pedidoVenda.PedidoItens.Select(BuscarPedidoItemDTO.FromDomain).ToList(),
            };
    }
}
