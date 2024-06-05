using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Pedidos.API.Models;
using MediatR;

namespace GDE.Pedidos.API.Application.Commands
{
    public class PedidoCompraCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPedidoCompraCommand, ValidationResult>
    {
        private readonly IPedidoCompraRepository _pedidoCompraRepository;
        private readonly IMessageBus _bus;

        public PedidoCompraCommandHandler(IPedidoCompraRepository pedidoCompraRepository, IMessageBus messageBus)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _bus = messageBus;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoCompraCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedidoCompra = MapearItens(message);

            _pedidoCompraRepository.Adicionar(pedidoCompra);

            var response = await AdicionarItensEstoque(message);

            if(!response.ValidationResult.IsValid)
                return response.ValidationResult;

            return await PersistirDados(_pedidoCompraRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> AdicionarItensEstoque(AdicionarPedidoCompraCommand pedidoCompra)
        {
            var pedidoItemCadastrado = pedidoCompra.PedidoItens.ConvertAll(i => new PedidoItemIntegrationEvent
            (
                i.ProdutoId,
                i.LocalId,
                i.NomeProduto,
                i.Comprimento,
                i.Largura,
                i.Altura,
                i.Quantidade,
                i.PrecoUnitario,
                i.PedidoCompraId,
                i.PedidoVendaId,
                i.PedidoTransferenciaId)
            );

            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(TipoMovimentacao.Entrada, pedidoItemCadastrado);

            try
            {
                return await _bus.RequestAsync<PedidoCadastradoIntegrationEvent, ResponseMessage>(pedidoCadastrado);
            }
            catch
            {
                throw;
            }

        }

        private PedidoCompra MapearItens(AdicionarPedidoCompraCommand message)
        {
            var itens = message.PedidoItens
                .ConvertAll(i => new PedidoItem(
                    i.ProdutoId,
                    i.LocalId,
                    i.Quantidade,
                    i.PrecoUnitario,
                    i.PedidoCompraId,
                    null,
                    null));

            return new PedidoCompra(message.NomeFornecedor, message.Timestamp, message.IdFuncionarioResponsavel, itens); 
        }
    }
}
