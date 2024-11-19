using GDE.Core.Data;

namespace GDE.Estoque.Domain
{
    public interface ILocalRepository : IRepository<Local>
    {
        Task<Local> ObterPorId(Guid id);
        Task<IEnumerable<Local>> ObterTodos();
        Task<(IEnumerable<Local>, int quantidadeTotal)> ObterTodosPaginado(int pageSize, int pageIndex);
        Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId);
        Task<LocalItem> ObterItemLocalPorProdutoId(Guid localId, Guid produtoId);
        Task<IEnumerable<LocalItem>> ObterProximosAoVencimento(DateTime compareDate);
        void Adicionar(Local local);
        void Remover(Local local);
        void Atualizar(Local local); 
        void AdicionarItem(LocalItem localItem);
        void RemoverItem(LocalItem localItem);
        void AtualizarItem(LocalItem localItem);
    }
}
