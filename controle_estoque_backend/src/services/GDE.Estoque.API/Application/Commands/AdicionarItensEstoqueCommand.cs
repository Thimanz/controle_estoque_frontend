using FluentValidation;
using GDE.Core.Messages;
using GDE.Estoque.API.Application.DTO;

namespace GDE.Estoque.API.Application.Commands
{
    public class AdicionarItensEstoqueCommand : Command
    {
        public List<LocalItemDTO> LocalItens { get; private set; }

        public AdicionarItensEstoqueCommand(List<LocalItemDTO> localItens)
        {
            LocalItens = localItens;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarItensEstoqueValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarItensEstoqueValidation : AbstractValidator<AdicionarItensEstoqueCommand>
        {
            public AdicionarItensEstoqueValidation()
            {
                RuleFor(c => c.LocalItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");
            }
        }
    }
}
