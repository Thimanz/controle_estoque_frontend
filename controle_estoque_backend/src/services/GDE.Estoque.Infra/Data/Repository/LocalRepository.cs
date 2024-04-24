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
            return await _context.Locais.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId)
        {
            return _context.Locais.Include(i => i.LocalItens).Where(l => l.LocalItens.Any(i => i.ProdutoId == produtoId));
        }

        public void AtualizarLocalItens(LocalItem localItem)
        {
            _context.LocalItens.Add(localItem);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
