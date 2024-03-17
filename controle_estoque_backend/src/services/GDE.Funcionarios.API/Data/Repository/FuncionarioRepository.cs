using GDE.Core.Data;
using GDE.Funcionarios.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GDE.Funcionarios.API.Data.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly FuncionariosContext _context;

        public FuncionarioRepository(FuncionariosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {
            return await _context.Funcionarios.AsNoTracking().ToListAsync();
        }

        public async Task<Funcionario> ObterPorCpf(string cpf)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void Adicionar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
