using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using GDE.Core.DomainObjects;

namespace GDE.Estoque.Domain
{
    public class Local : Entity, IAggregateRoot
    {
        public Local(string? nome, string? endereco, decimal altura, decimal largura, decimal comprimento, List<LocalItem> localItens)
        {
            Nome = nome;
            Endereco = endereco;
            Dimensoes = new Dimensoes(comprimento, largura, altura);
            _localItens = localItens;
        }

        //EF ctor
        public Local()
        {
            _localItens = new List<LocalItem>();
        }

        public string? Nome { get; private set; }
        public string? Endereco { get; private set; }
        public Dimensoes Dimensoes { get; private set; }

        public readonly List<LocalItem> _localItens;
        public IReadOnlyCollection<LocalItem> LocalItens => _localItens;

        [NotMapped]
        public Dimensoes EspacoLivre => CalcularEspacoLivre();

        [NotMapped]
        public decimal EspacoTotal => (Dimensoes.Altura * Dimensoes.Largura * Dimensoes.Comprimento);

        [NotMapped]
        public decimal EspacoLivreCalculado => (EspacoLivre.Altura * EspacoLivre.Largura * EspacoLivre.Comprimento);


        public void AdicionarItem(LocalItem item)
        {
            if (!VerificarEspacoLivre(item))
                throw new DomainException("O local não possui espaço suficiente");

            item.AssociarLocal(Id);
            _localItens.Add(item);
        }

        public void RemoverItem(LocalItem item)
        {
            if (!ItemExistente(item))
                throw new DomainException("Item não encontrado no local");

            _localItens.Remove(item);
        }

        private Dimensoes CalcularEspacoLivre()
        {
            var alturaLivre = Dimensoes.Altura - LocalItens.Sum(i => i.Dimensoes.Altura * i.Quantidade);
            var larguraLivre = Dimensoes.Largura - LocalItens.Sum(i => i.Dimensoes.Largura * i.Quantidade);
            var comprimentoLivre = Dimensoes.Comprimento - LocalItens.Sum(i => i.Dimensoes.Comprimento * i.Quantidade);

            return new Dimensoes(comprimentoLivre, larguraLivre, alturaLivre);
        }

        public bool VerificarEspacoLivre(LocalItem item)
        {
            return (EspacoLivre.Altura - (item.Dimensoes.Altura * item.Quantidade) >= 0
                && EspacoLivre.Largura - (item.Dimensoes.Largura * item.Quantidade) >= 0
                && EspacoLivre.Comprimento - (item.Dimensoes.Comprimento * item.Quantidade) >= 0);
        }

        public List<LocalItem> ObterPorProdutoId(Guid produtoId)
        {
            return LocalItens.Where(p => p.ProdutoId == produtoId).ToList();
        }

        public int ObterQuantidadePorProduto(Guid produtoId)
        {
            return LocalItens.Where(p => p.ProdutoId == produtoId).Sum(i => i.Quantidade);
        }

        public bool ItemExistente(LocalItem item)
        {
            return LocalItens.Any(i => i.ProdutoId == item.ProdutoId);
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarEndereco(string endereco)
        {
            Endereco = endereco;
        }

        public void AlterarDimensoes(decimal comprimento, decimal largura, decimal altura)
        {
            Dimensoes = new Dimensoes(comprimento, largura, altura);
        }
    }
}
