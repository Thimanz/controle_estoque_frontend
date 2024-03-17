using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Core.Utils;
using GDE.Funcionarios.API.Application.Events;
using GDE.Funcionarios.API.Models;
using MediatR;

namespace GDE.Funcionarios.API.Application.Commands
{
    public class FuncionarioCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarFuncionarioCommand, ValidationResult>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioCommandHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarFuncionarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var funcionario = new Funcionario(message.Id, message.Nome, message.Cpf.ApenasNumeros(message.Cpf), message.Email);

            var funcionarioExistente = await _funcionarioRepository.ObterPorCpf(funcionario.Cpf.Numero);

            if (funcionarioExistente is not null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _funcionarioRepository.Adicionar(funcionario);

            funcionario.AdicionarEvento(new FuncionarioRegistradoEvent(message.Id, message.Nome, message.Cpf, message.Email));

            return await PersistirDados(_funcionarioRepository.UnitOfWork);
        }
    }
}
