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

        public ProdutoMovimentadoEvent() { }
    
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public TipoMovimentacao Tipo { get; set; }

    }
}
