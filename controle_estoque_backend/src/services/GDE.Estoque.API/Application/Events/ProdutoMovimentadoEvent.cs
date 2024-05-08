using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.Events
{
    public class ProdutoMovimentadoEvent : Event
    {
        public ProdutoMovimentadoEvent(Guid produtoId, int quantidade, TipoMovimentacao tipo)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Tipo = tipo;
        }

        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public TipoMovimentacao Tipo {  get; private set; }

    }
}
