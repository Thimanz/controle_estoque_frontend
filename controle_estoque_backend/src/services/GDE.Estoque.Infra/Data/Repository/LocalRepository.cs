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

        public Task<IEnumerable<Local>> ObterListaPorProdutoId(Guid produtoId)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Local local)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
