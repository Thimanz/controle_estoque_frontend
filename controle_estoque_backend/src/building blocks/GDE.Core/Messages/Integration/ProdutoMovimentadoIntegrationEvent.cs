namespace GDE.Core.Messages.Integration
{
    public class ProdutoMovimentadoIntegrationEvent : IntegrationEvent
    {
        public ProdutoMovimentadoIntegrationEvent(Guid produtoId, int quantidade, TipoMovimentacao tipo)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Tipo = tipo;
        }

        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public TipoMovimentacao Tipo { get; private set; }

    }
}
