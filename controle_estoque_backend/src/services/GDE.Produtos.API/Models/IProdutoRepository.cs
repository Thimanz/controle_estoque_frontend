using GDE.Core.Data;

namespace GDE.Produtos.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto?> ObterPorId(Guid id);
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Remover(Produto produto);
    }
}
