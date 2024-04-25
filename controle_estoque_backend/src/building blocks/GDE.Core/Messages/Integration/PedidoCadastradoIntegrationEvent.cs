namespace GDE.Core.Messages.Integration
{
    public class PedidoCadastradoIntegrationEvent : IntegrationEvent
    {
        public PedidoCadastradoIntegrationEvent(List<PedidoItemIntegrationEvent> itens)
        {
            Itens = itens;
        }

        public List<PedidoItemIntegrationEvent> Itens { get; private set; }

        public class PedidoItemIntegrationEvent
        {
            public PedidoItemIntegrationEvent(Guid produtoId, Guid localId, int quantidade, decimal precoUnitario, Guid? pedidoCompraId, Guid? pedidoVendaId)
            {
                ProdutoId = produtoId;
                LocalId = localId;
                Quantidade = quantidade;
                PrecoUnitario = precoUnitario;
                PedidoCompraId = pedidoCompraId;
                PedidoVendaId = pedidoVendaId;
            }

            public Guid ProdutoId { get; private set; }
            public Guid LocalId { get; private set; }
            public int Quantidade { get; private set; }
            public decimal PrecoUnitario { get; private set; }
            public Guid? PedidoCompraId { get; private set; }
            public Guid? PedidoVendaId { get; private set; }

        }
    }
}
