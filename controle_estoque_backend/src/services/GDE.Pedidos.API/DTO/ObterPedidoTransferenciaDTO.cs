using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class ObterPedidoTransferenciaDTO
    {
        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public Guid IdLocalDestino { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; private set; } = new List<BuscarPedidoItemDTO>();

        public static ObterPedidoTransferenciaDTO FromDomain(PedidoTransferencia pedidoTransferencia) =>
            new ObterPedidoTransferenciaDTO
            {
                Id = pedidoTransferencia.Id,
                IdFuncionarioResponsavel = pedidoTransferencia.IdFuncionarioResponsavel,
                IdLocalDestino = pedidoTransferencia.IdLocalDestino,
                Numero = pedidoTransferencia.Numero,
                PrecoTotal = pedidoTransferencia.PrecoTotal,
                DataCriacao = pedidoTransferencia.DataCriacao,
                PedidoItens = pedidoTransferencia.PedidoItens.Select(BuscarPedidoItemDTO.FromDomain).ToList(),
            };
    }
}
