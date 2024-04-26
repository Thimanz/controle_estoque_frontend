using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Pedidos.API.Models;
using MediatR;
using static GDE.Core.Messages.Integration.PedidoCadastradoIntegrationEvent;

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

            _pedidoCompraRepository.AdicionarPedidoCompra(pedidoCompra);

            await AdicionarItensEstoque(pedidoCompra);

            return await PersistirDados(_pedidoCompraRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> AdicionarItensEstoque(PedidoCompra pedidoCompra)
        {
            var pedidoItemCadastrado = pedidoCompra.PedidoItens.ConvertAll(i => new PedidoItemIntegrationEvent
            (
                i.ProdutoId,
                i.LocalId,
                i.Quantidade,
                i.PrecoUnitario,
                i.PedidoCompraId,
                i.PedidoVendaId)
            );

            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(pedidoItemCadastrado);

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
                    null));

            return new PedidoCompra(message.NomeFornecedor, message.IdFuncionarioResponsavel, itens); 
        }
    }
}
