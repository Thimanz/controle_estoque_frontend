using GDE.Core.Messages.Integration;
using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class BuscarPedidosDto
    {
        public Guid Id { get; set; }
        public Guid IdFuncionarioResponsavel { get; set; }
        public int Numero { get; set; }
        public decimal PrecoTotal { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Tipo {  get; set; }

        public static BuscarPedidosDto FromPedidoCompra(PedidoCompra pedidoCompra)
        {
            return new BuscarPedidosDto
            {
                Id = pedidoCompra.Id,
                IdFuncionarioResponsavel = pedidoCompra.IdFuncionarioResponsavel,
                Numero = pedidoCompra.Numero,
                PrecoTotal = pedidoCompra.PrecoTotal,
                Quantidade = pedidoCompra.Quantidade(),
                DataCriacao = pedidoCompra.DataCriacao,
                Tipo = (int)TipoMovimentacao.Entrada
            };
        }

        public static BuscarPedidosDto FromPedidoVenda(PedidoVenda pedidoVenda)
        {
            return new BuscarPedidosDto
            {
                Id = pedidoVenda.Id,
                IdFuncionarioResponsavel = pedidoVenda.IdFuncionarioResponsavel,
                Numero = pedidoVenda.Numero,
                PrecoTotal = pedidoVenda.PrecoTotal,
                Quantidade = pedidoVenda.Quantidade(),
                DataCriacao = pedidoVenda.DataCriacao,
                Tipo = (int)TipoMovimentacao.Saida
            };
        }

        public static BuscarPedidosDto FromPedidoTransferencia(PedidoTransferencia pedidoTransferencia)
        {
            return new BuscarPedidosDto
            {
                Id = pedidoTransferencia.Id,
                IdFuncionarioResponsavel = pedidoTransferencia.IdFuncionarioResponsavel,
                Numero = pedidoTransferencia.Numero,
                PrecoTotal = pedidoTransferencia.PrecoTotal,
                Quantidade = pedidoTransferencia.Quantidade(),
                DataCriacao = pedidoTransferencia.DataCriacao,
                Tipo = (int)TipoMovimentacao.Transferencia
            };
        }
    }
}
