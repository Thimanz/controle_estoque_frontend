using GDE.Core.DomainObjects;

namespace GDE.Estoque.Domain
{
    public class Local : Entity, IAggregateRoot
    {
        public Local(string? nome, decimal altura, decimal largura, decimal comprimento, List<Item> itens)
        {
            Nome = nome;
            EspacoLivre = new Dimensoes(comprimento, largura, altura);
            _items = itens;

            CalcularEspacoLivre();
        }

        public string? Nome { get; private set; }
        public Dimensoes EspacoLivre { get; private set; }
        public readonly List<Item> _items;
        public IReadOnlyCollection<Item> Items => _items;

        public void AdicionarItem(Item item)
        {
            if(!VerificarEspacoLivre(item))
                throw new DomainException("O local não possui espaço suficiente");

            item.AssociarLocal(Id);
            _items.Add(item);

            CalcularEspacoLivre();
        }

        public void RemoverItem(Item item)
        {
            if (!ItemExistente(item))
                throw new DomainException("Item não encontrado no local");

            _items.Remove(ObterPorProdutoId(item.ProdutoId));

            CalcularEspacoLivre();
        }

        private void CalcularEspacoLivre()
        {
            var alturaLivre = EspacoLivre.Altura - Items.Sum(i => i.Dimensoes.Altura);
            var larguraLivre = EspacoLivre.Largura - Items.Sum(i => i.Dimensoes.Largura);
            var comprimentoLivre = EspacoLivre.Comprimento - Items.Sum(i => i.Dimensoes.Comprimento);

            EspacoLivre = new Dimensoes(comprimentoLivre, larguraLivre, alturaLivre);
        }

        public bool VerificarEspacoLivre(Item item)
        {
            return (EspacoLivre.Altura - item.Dimensoes.Altura >= 0
                && EspacoLivre.Largura - item.Dimensoes.Largura >= 0
                && EspacoLivre.Comprimento - item.Dimensoes.Comprimento >= 0);
        }

        public Item ObterPorProdutoId(Guid produtoId)
        {
            return Items.FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        public bool ItemExistente(Item item)
        {
            return Items.Any(i => i.ProdutoId ==  item.ProdutoId);
        }
    }
}
