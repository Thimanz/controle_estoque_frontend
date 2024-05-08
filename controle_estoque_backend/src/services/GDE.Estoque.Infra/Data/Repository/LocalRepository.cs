using GDE.Core.Data;
using GDE.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.Infra.Data.Repository
{
    public class LocalRepository : ILocalRepository
    {
        public EstoqueContext _context;

        public LocalRepository(EstoqueContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<Local> ObterPorId(Guid id)
        {
            return await _context.Locais.Include(i => i.LocalItens).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId)
        {
            return _context.Locais.Include(i => i.LocalItens).Where(l => l.LocalItens.Any(i => i.ProdutoId == produtoId));
        }

        public async Task<LocalItem> ObterItemLocalPorProdutoId(Guid localId, Guid produtoId)
        {
            return await _context.LocalItens.FirstOrDefaultAsync(i => i.LocalId == localId && i.ProdutoId == produtoId);
        }

        public void AdicionarItem(LocalItem localItem)
        {
            _context.Add(localItem);
        }

        public void RemoverItem(LocalItem localItem)
        {
            _context.Remove(localItem);
        }

        public void AtualizarItem(LocalItem localItem)
        {
            _context.Update(localItem);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
