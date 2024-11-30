using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Pedidos.API.Data.Repository;
using GDE.Pedidos.API.Models;
using MediatR;

namespace GDE.Pedidos.API.Application.Commands
{
    public class PedidoTransferenciaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPedidoTransferenciaCommand, ValidationResult>
    {
        private readonly IPedidoTransferenciaRepository _pedidoTransferenciaRepository;
        private readonly IMessageBus _bus;

        public PedidoTransferenciaCommandHandler(IPedidoTransferenciaRepository pedidoTransferenciaRepository, IMessageBus bus)
        {
            _pedidoTransferenciaRepository = pedidoTransferenciaRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoTransferenciaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedidoTransferencia = MapearItens(message);

            _pedidoTransferenciaRepository.Adicionar(pedidoTransferencia);

            if (!message.ValidationResult.IsValid)
                return message.ValidationResult;

            var response = await TransferirItensEstoque(message);

            if (!response.ValidationResult.IsValid)
                return response.ValidationResult;

            return await PersistirDados(_pedidoTransferenciaRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> TransferirItensEstoque(AdicionarPedidoTransferenciaCommand pedidoTransferencia)
        {
            var pedidoItemCadastrado = pedidoTransferencia.PedidoItens.ConvertAll(i => new PedidoItemIntegrationEvent
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

            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(TipoMovimentacao.Transferencia, pedidoItemCadastrado, pedidoTransferencia.IdLocalDestino);

            try
            {
                return await _bus.RequestAsync<PedidoCadastradoIntegrationEvent, ResponseMessage>(pedidoCadastrado);
            }
            catch
            {
                throw;
            }
        }

        private PedidoTransferencia MapearItens(AdicionarPedidoTransferenciaCommand message)
        {
            var itens = message.PedidoItens
                .ConvertAll(i => new PedidoItem(
                    i.ProdutoId,
                    i.LocalId,
                    i.Quantidade,
                    i.PrecoUnitario,
                    null,
                    null,
                    i.PedidoTransferenciaId,
                    i.DataValidade));

            return new PedidoTransferencia(message.IdLocalDestino!.Value, message.Timestamp, message.IdFuncionarioResponsavel, itens);
        }
    }
}
