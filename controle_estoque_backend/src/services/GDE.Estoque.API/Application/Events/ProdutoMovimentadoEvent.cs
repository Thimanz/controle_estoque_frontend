using GDE.Core.Messages;

namespace GDE.Estoque.API.Application.Events
{
    public class ProdutoMovimentadoEvent : Event
    {
        public ProdutoMovimentadoEvent(Guid produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

    }
}
