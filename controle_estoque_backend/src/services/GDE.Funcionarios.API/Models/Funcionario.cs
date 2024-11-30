using GDE.Core.DomainObjects;

namespace GDE.Funcionarios.API.Models
{
    public class Funcionario : Entity, IAggregateRoot
    {
        public string? Nome { get; private set; }
        public Cpf? Cpf { get; private set; }
        public Email? Email { get; private set; }
        public Cargo? Cargo { get; private set; }
        public bool Excluido { get; private set; }

        //Construtor do EntityFramework
        public Funcionario() { }

        public Funcionario(Guid id, string? nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            Cpf = new Cpf(cpf);
            Email = new Email(email);
            Excluido = false;
        }

        public void TrocarEmail(string email) 
        {
            Email = new Email(email);
        }

        public void TrocarCargo(Cargo cargo)
        {
            Cargo = cargo;
        }
    }
}
