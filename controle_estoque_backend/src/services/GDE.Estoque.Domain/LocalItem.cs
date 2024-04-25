using GDE.Core.DomainObjects;

namespace GDE.Estoque.Domain
{
    public class LocalItem : Entity
    {
        public LocalItem(Guid produtoId, string? nome, Dimensoes dimensoes, decimal preco, int quantidade)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Dimensoes = dimensoes;
            Preco = preco;
            Quantidade = quantidade;
        }

        //EF ctor
        public LocalItem() { }

        public Guid LocalId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string? Nome { get; private set; }
        public Dimensoes Dimensoes { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        // EF Rel.
        public Local Local { get; set; }

        internal void AssociarLocal(Guid localId) 
        {
            LocalId = localId;
        }
    }
}