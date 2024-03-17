using GDE.Core.Messages;

namespace GDE.Funcionarios.API.Application.Events
{
    public class FuncionarioRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public FuncionarioRegistradoEvent(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}
