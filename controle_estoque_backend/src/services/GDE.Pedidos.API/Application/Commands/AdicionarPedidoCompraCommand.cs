using FluentValidation;
using GDE.Core.Messages;
using GDE.Pedidos.API.Application.DTO;

namespace GDE.Pedidos.API.Application.Commands
{
    public class AdicionarPedidoCompraCommand : Command
    {
        public AdicionarPedidoCompraCommand(Guid idFuncionarioResponsavel, string? nomeFornecedor, List<PedidoItemDTO> pedidoItens)
        {
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            NomeFornecedor = nomeFornecedor;
            PedidoItens = pedidoItens;
        }

        public Guid IdFuncionarioResponsavel { get; set; }
        public string? NomeFornecedor { get; private set; }
        public List<PedidoItemDTO> PedidoItens { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPedidoCompraValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarPedidoCompraValidation : AbstractValidator<AdicionarPedidoCompraCommand>
        {
            public AdicionarPedidoCompraValidation()
            {
                RuleFor(c => c.IdFuncionarioResponsavel)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Funcionário Responsável não informado");

                RuleFor(c => c.NomeFornecedor)
                    .NotEmpty()
                    .WithMessage("Nome do Fornecedor não informado");

                RuleFor(c => c.PedidoItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");

            }
        }
    }
}
