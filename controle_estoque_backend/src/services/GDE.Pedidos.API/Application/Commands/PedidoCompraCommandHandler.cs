using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.Pedidos.API.Models;
using MassTransit;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GDE.Pedidos.API.Application.Commands
{
    public class PedidoCompraCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPedidoCompraCommand, ValidationResult>
    {
        private readonly IPedidoCompraRepository _pedidoCompraRepository;
        private readonly IRequestClient<PedidoCadastradoIntegrationEvent> _requestClient;

        public PedidoCompraCommandHandler(IPedidoCompraRepository pedidoCompraRepository,
            IRequestClient<PedidoCadastradoIntegrationEvent> pedidoCadastradorequestClient)
        {
            _pedidoCompraRepository = pedidoCompraRepository;
            _requestClient = pedidoCadastradorequestClient;
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
                i.PedidoTransferenciaId,
                null)
            );

            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(TipoMovimentacao.Entrada, pedidoItemCadastrado);

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

        private PedidoCompra MapearItens(AdicionarPedidoCompraCommand message)
        {
            var itens = message.PedidoItens
                .ConvertAll(i => new PedidoItem(
                    i.ProdutoId,
                    i.LocalId,
                    i.LocalNome,
                    i.Quantidade,
                    i.PrecoUnitario,
                    i.PedidoCompraId,
                    null,
                    null,
                    i.DataValidade,
                    i.Imagem));

            return new PedidoCompra(message.NomeFornecedor, message.Timestamp, message.IdFuncionarioResponsavel, itens); 
        }
    }
}
