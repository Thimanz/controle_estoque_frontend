using FluentValidation;
using GDE.Core.Messages;
using GDE.Funcionarios.API.Models;

namespace GDE.Funcionarios.API.Application.Commands
{
    public class RegistrarFuncionarioCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }

        public RegistrarFuncionarioCommand(Guid id, string nome, string cpf, string email)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarFuncionarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class RegistrarFuncionarioValidation : AbstractValidator<RegistrarFuncionarioCommand>
    {
        public RegistrarFuncionarioValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do Funcionario inválido.");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do funcionário não foi informado.");

            RuleFor(c => c.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O CPF informado não é válido.");
            
            RuleFor(c => c.Email)
                .Must(TerEmailValido)
                .WithMessage("O e-mail informado não é válido.");
        }

        protected static bool TerCpfValido(string cpf)
        {
            return Cpf.Validar(cpf);
        }

        protected static bool TerEmailValido(string email)
        {
            return Email.Validar(email);
        }
    }
}
