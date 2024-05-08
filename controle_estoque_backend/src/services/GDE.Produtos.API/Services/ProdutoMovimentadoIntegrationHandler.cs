using FluentValidation.Results;
using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Produtos.API.Data;

namespace GDE.Produtos.API.Services
{
    public class ProdutoMovimentadoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public ProdutoMovimentadoIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        //private void SetSubscribers()
        //{
        //    _bus.SubscribeAsync<ProdutoMovimentadoIntegrationEvent>("Estoque Alterado", async request =>
        //        await AlterarQuantidadeEstoque(request));
        //}

        private void SetResponder()
        {
            _bus.RespondAsync<ProdutoMovimentadoIntegrationEvent, ResponseMessage>(AlterarQuantidadeEstoque);
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

        private async Task<ResponseMessage> AlterarQuantidadeEstoque(ProdutoMovimentadoIntegrationEvent message)
        {
            ValidationResult result;

            if (message.Tipo == TipoMovimentacao.Transferencia) return new ResponseMessage(new ValidationResult());

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProdutoContext>();

            var produto = await context.Produtos.FindAsync(message.ProdutoId);

            if (produto is null)
            {
                message.AdicionarErro($"Produto {message.ProdutoId} não cadastrado");
                return new ResponseMessage(message.ValidationResult);
            }

            if (message.Tipo == TipoMovimentacao.Entrada)
                produto.AdicionarEstoque(message.Quantidade);
            else
                produto.RetirarEstoque(message.Quantidade);

            context.Produtos.Update(produto);

            result = await message.PersistirDados(context);


            return new ResponseMessage(result);
        }
    }
}
