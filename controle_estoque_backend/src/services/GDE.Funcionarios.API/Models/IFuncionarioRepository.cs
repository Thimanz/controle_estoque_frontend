using GDE.Core.Data;

namespace GDE.Funcionarios.API.Models
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        void Adicionar(Funcionario funcionario);


        Task<IEnumerable<Funcionario>> ObterTodos();
        Task<Funcionario> ObterPorCpf(string cpf);
    }
}
