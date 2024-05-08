using FluentValidation;
using GDE.Core.Messages;
using GDE.Pedidos.API.Application.DTO;

namespace GDE.Pedidos.API.Application.Commands
{
    public class AdicionarPedidoVendaCommand : Command
    {
        public AdicionarPedidoVendaCommand(Guid idFuncionarioResponsavel, string? nomeCliente, List<PedidoItemDTO> pedidoItens)
        {
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            NomeCliente = nomeCliente;
            PedidoItens = pedidoItens;
        }

        public Guid IdFuncionarioResponsavel { get; set; }
        public string? NomeCliente { get; private set; }

        public List<PedidoItemDTO> PedidoItens { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPedidoVendaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarPedidoVendaValidation : AbstractValidator<AdicionarPedidoVendaCommand>
        {
            public AdicionarPedidoVendaValidation()
            {
                RuleFor(c => c.IdFuncionarioResponsavel)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Funcionário Responsável não informado");

                RuleFor(c => c.NomeCliente)
                    .NotEmpty()
                    .WithMessage("Nome do cliente não informado");

                RuleFor(c => c.PedidoItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");

            }
        }
    }
}
