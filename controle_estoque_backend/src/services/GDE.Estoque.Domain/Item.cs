using GDE.Core.DomainObjects;

namespace GDE.Estoque.Domain
{
    public class Item : Entity
    {
        public Item(Guid produtoId, string? nome, Dimensoes dimensoes)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Dimensoes = dimensoes;
        }

        public Guid LocalId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string? Nome { get; private set; }
        public Dimensoes Dimensoes { get; private set; }

        internal void AssociarLocal(Guid localId)
        {
            LocalId = localId;
        }
    }
}