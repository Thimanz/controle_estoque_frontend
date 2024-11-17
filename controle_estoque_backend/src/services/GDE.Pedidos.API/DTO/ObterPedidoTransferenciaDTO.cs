using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class ObterPedidoTransferenciaDTO
    {
        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public LocalDTO? LocalDestino { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public string? DataCriacao { get; private set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; private set; } = new List<BuscarPedidoItemDTO>();

        public static ObterPedidoTransferenciaDTO FromDomain(PedidoTransferencia pedidoTransferencia) =>
            new ObterPedidoTransferenciaDTO
            {
                Id = pedidoTransferencia.Id,
                IdFuncionarioResponsavel = pedidoTransferencia.IdFuncionarioResponsavel,
                LocalDestino = new(pedidoTransferencia.IdLocalDestino, pedidoTransferencia.NomeLocalDestino),
                Numero = pedidoTransferencia.Numero,
                PrecoTotal = pedidoTransferencia.PrecoTotal,
                DataCriacao = pedidoTransferencia.DataCriacao.ToString("dd/MM/yyyy"),
                PedidoItens = pedidoTransferencia.PedidoItens.Select(BuscarPedidoItemDTO.FromDomain).ToList(),
            };
    }
}
