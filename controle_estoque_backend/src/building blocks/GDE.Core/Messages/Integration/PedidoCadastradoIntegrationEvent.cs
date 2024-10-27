namespace GDE.Core.Messages.Integration
{
    public class PedidoCadastradoIntegrationEvent : IntegrationEvent
    {
        public PedidoCadastradoIntegrationEvent(TipoMovimentacao tipo, List<PedidoItemIntegrationEvent> itens, Guid? idLocalDestino = null)
        {
            Tipo = tipo;
            Itens = itens;
            IdLocalDestino = idLocalDestino;
        }

        public PedidoCadastradoIntegrationEvent() { }
        
        public TipoMovimentacao Tipo { get; set; }
        public List<PedidoItemIntegrationEvent> Itens { get; set; }
        public Guid? IdLocalDestino { get; set; }
    }

    public class PedidoItemIntegrationEvent
    {
        public PedidoItemIntegrationEvent(Guid produtoId, Guid localId, string nomeProduto, decimal comprimento, decimal largura, decimal altura,
            int quantidade, decimal precoUnitario, Guid? pedidoCompraId, Guid? pedidoVendaId, Guid? pedidoTransferenciaId)
        {
            ProdutoId = produtoId;
            LocalId = localId;
            NomeProduto = nomeProduto;
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            PedidoCompraId = pedidoCompraId;
            PedidoVendaId = pedidoVendaId;
            PedidoTransferenciaId = pedidoTransferenciaId;
        }

        public PedidoItemIntegrationEvent() { }
        
        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid? PedidoTransferenciaId { get; set; }
    }

    public enum TipoMovimentacao
    {
        Entrada,
        Saida,
        Transferencia
    }
}
