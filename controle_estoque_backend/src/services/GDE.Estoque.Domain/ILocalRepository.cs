using System.Data.Common;
using GDE.Core.Data;

namespace GDE.Estoque.Domain
{
    public interface ILocalRepository : IRepository<Local>
    {
        Task<Local> ObterPorId(Guid id);
        Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId);
        void AtualizarLocalItens(LocalItem localItem);
    }
}
