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
            _bus.RespondAsync<PedidoCadastradoIntegrationEvent, ResponseMessage>(AdicionarItensEstoque);
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

        private async Task<ResponseMessage> AdicionarItensEstoque(PedidoCadastradoIntegrationEvent message)
        {
            var adicionarItensEstoqueCommand = new AdicionarItensEstoqueCommand(
                message.Itens.ConvertAll(m => new LocalItemDTO(
                m.LocalId,
                m.ProdutoId,
                m.PrecoUnitario,
                m.Quantidade
            )));

            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
                result = await mediator.EnviarComando(adicionarItensEstoqueCommand);
            }

            return new ResponseMessage(result);
        }
    }
}
