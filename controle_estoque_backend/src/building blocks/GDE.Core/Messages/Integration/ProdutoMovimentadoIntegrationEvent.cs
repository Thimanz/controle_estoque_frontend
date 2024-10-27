namespace GDE.Core.Messages.Integration
{
    public class ProdutoMovimentadoIntegrationEvent : IntegrationEvent
    {
        public static string QueueName => "gde.produto.movimentado";

        public ProdutoMovimentadoIntegrationEvent(Guid produtoId, int quantidade, TipoMovimentacao tipo)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Tipo = tipo;
        }

        public ProdutoMovimentadoIntegrationEvent() { }
        
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public TipoMovimentacao Tipo { get; set; }

    }
}
