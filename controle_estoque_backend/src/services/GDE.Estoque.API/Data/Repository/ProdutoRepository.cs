using GDE.Core.Data;
using GDE.Estoque.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EstoqueContext _context;

        public ProdutoRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto?> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
