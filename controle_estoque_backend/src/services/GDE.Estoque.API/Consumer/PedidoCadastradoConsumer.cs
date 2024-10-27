using GDE.Core.Mediator;
using GDE.Core.Messages.Integration;
using GDE.Estoque.API.Application.Commands;
using GDE.Estoque.API.Application.DTO;
using MassTransit;

namespace GDE.Estoque.API.Consumer
{
    public class PedidoCadastradoConsumer : IConsumer<PedidoCadastradoIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public PedidoCadastradoConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<PedidoCadastradoIntegrationEvent> context)
        {
            var movimentarItensEstoqueCommand = new MovimentarItensEstoqueCommand((TipoMovimentacao)context.Message.Tipo.GetHashCode(),
                context.Message.Itens.ConvertAll(m => new LocalItemDTO(
                m.LocalId,
                m.ProdutoId,
                m.NomeProduto,
                m.Comprimento,
                m.Largura,
                m.Altura,
                m.PrecoUnitario,
                m.Quantidade,
                m.PedidoCompraId,
                m.PedidoVendaId
                )),
                context.Message.IdLocalDestino);

            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
            var result = await mediator!.EnviarComando(movimentarItensEstoqueCommand);

            await context.RespondAsync(new ResponseMessage(result));
        }
    }
}
