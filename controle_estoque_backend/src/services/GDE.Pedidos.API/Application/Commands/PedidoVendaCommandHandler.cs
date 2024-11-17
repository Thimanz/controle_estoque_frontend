using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.Pedidos.API.Models;
using MassTransit;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GDE.Pedidos.API.Application.Commands
{
    public class PedidoVendaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPedidoVendaCommand, ValidationResult>
    {
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private readonly IRequestClient<PedidoCadastradoIntegrationEvent> _requestClient;

        public PedidoVendaCommandHandler(IPedidoVendaRepository pedidoVendaRepository, 
            IRequestClient<PedidoCadastradoIntegrationEvent> pedidoCadastradorequestClient)
        {
            _pedidoVendaRepository = pedidoVendaRepository;
            _requestClient = pedidoCadastradorequestClient;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoVendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedidoVenda = MapearItens(message);

            _pedidoVendaRepository.Adicionar(pedidoVenda);

            if (!message.ValidationResult.IsValid)
                return message.ValidationResult;

            var response = await RemoverItensEstoque(message);

            if(!response.ValidationResult.IsValid)
                return response.ValidationResult;

            return await PersistirDados(_pedidoVendaRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> RemoverItensEstoque(AdicionarPedidoVendaCommand pedidoVenda)
        {
            var pedidoItemCadastrado = pedidoVenda.PedidoItens.ConvertAll(i => new PedidoItemIntegrationEvent
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
                i.PedidoTransferenciaId,
                null)
            );


            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(TipoMovimentacao.Saida, pedidoItemCadastrado);

            try
            {
                var response = await _requestClient.GetResponse<ResponseMessage>(pedidoCadastrado);

                return response.Message;
            }
            catch
            {
                throw;
            }
        }

        private PedidoVenda MapearItens(AdicionarPedidoVendaCommand message)
        {
            var itens = message.PedidoItens
                .ConvertAll(i => new PedidoItem(
                    i.ProdutoId,
                    i.LocalId,
                    i.LocalNome,
                    i.Quantidade,
                    i.PrecoUnitario,
                    null,
                    i.PedidoVendaId,
                    null,
                    i.DataValidade));

            return new PedidoVenda(message.NomeCliente, message.Timestamp, message.IdFuncionarioResponsavel, itens);
        }
    }
}
