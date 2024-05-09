using System.Data.Common;
using GDE.Core.Data;

namespace GDE.Estoque.Domain
{
    public interface ILocalRepository : IRepository<Local>
    {
        Task<Local> ObterPorId(Guid id);
        Task<IEnumerable<Local>> ObterTodos();
        Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId);
        Task<LocalItem> ObterItemLocalPorProdutoId(Guid localId, Guid produtoId);
        void AdicionarItem(LocalItem localItem);
        void RemoverItem(LocalItem localItem);
        void AtualizarItem(LocalItem localItem);
    }
}
