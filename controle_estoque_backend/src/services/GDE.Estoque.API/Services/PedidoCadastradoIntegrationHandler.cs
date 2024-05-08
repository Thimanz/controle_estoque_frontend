using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Core.Messages.Integration;
using GDE.Estoque.API.Application.Commands;
using GDE.Estoque.API.Application.DTO;
using GDE.MessageBus;

namespace GDE.Estoque.API.Services
{
    public class PedidoCadastradoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PedidoCadastradoIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<PedidoCadastradoIntegrationEvent, ResponseMessage>(MovimentarItensEstoque);
            _bus.AdvancedBus.Connected += OnConnect!;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object sender, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> MovimentarItensEstoque(PedidoCadastradoIntegrationEvent message)
        {
            var movimentarItensEstoqueCommand = new MovimentarItensEstoqueCommand((TipoMovimentacao)message.Tipo.GetHashCode(),
                message.Itens.ConvertAll(m => new LocalItemDTO(
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
                message.IdLocalDestino);

            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
                result = await mediator.EnviarComando(movimentarItensEstoqueCommand);
            }

            return new ResponseMessage(result);
        }
    }
}
