namespace GDE.Core.Messages.Integration
{
    public class ProdutoMovimentadoIntegrationEvent : IntegrationEvent
    {
        public ProdutoMovimentadoIntegrationEvent(Guid produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

    }
}
