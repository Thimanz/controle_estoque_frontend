using GDE.Core.Messages.Integration;
using GDE.Produtos.API.Data;
using MassTransit;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace GDE.Produtos.API.Consumers
{
    public class ProdutoMovimentadoConsumer : IConsumer<ProdutoMovimentadoIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ProdutoMovimentadoConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<ProdutoMovimentadoIntegrationEvent> consumeContext)
        {
            var message = consumeContext.Message;

            if (message.Tipo == TipoMovimentacao.Transferencia)
            {
                await consumeContext.RespondAsync(new ResponseMessage(new ValidationResult()));
                return;
            }
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProdutoContext>();

            var produto = await context.Produtos.FindAsync(message.ProdutoId);

            if (produto is null)
            {
                message.AdicionarErro($"Produto {message.ProdutoId} não cadastrado");
                await consumeContext.RespondAsync(new ResponseMessage(message.ValidationResult!));
                return;
            }

            if (message.Tipo == TipoMovimentacao.Entrada)
                produto.AdicionarEstoque(message.Quantidade);
            else
                produto.RetirarEstoque(message.Quantidade);

            context.Produtos.Update(produto);

            var result = await message.PersistirDados(context);

            await consumeContext.RespondAsync(new ResponseMessage((result)));
        }
    }
}
