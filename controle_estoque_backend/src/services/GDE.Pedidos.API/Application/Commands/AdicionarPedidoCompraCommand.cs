using FluentValidation;
using GDE.Core.Messages;
using GDE.Pedidos.API.Application.DTO;

namespace GDE.Pedidos.API.Application.Commands
{
    public class AdicionarPedidoCompraCommand : Command
    {
        public Guid IdFuncionarioResponsavel { get; set; }
        public string? NomeFornecedor { get; set; }
        public List<PedidoItemDTO> PedidoItens { get; set; }

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
