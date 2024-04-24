using FluentValidation;
using GDE.Core.Messages;
using GDE.Estoque.API.Application.DTO;

namespace GDE.Estoque.API.Application.Commands
{
    public class AdicionarItensEstoqueCommand : Command
    {
        public Guid LocalId { get; set; }
        public List<LocalItemDTO> LocalItens { get; set; }
        
        public class AdicionarItensEstoqueValidation : AbstractValidator<AdicionarItensEstoqueCommand>
        {
            public AdicionarItensEstoqueValidation()
            {
                RuleFor(c => c.LocalId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Local não informado");

                RuleFor(c => c.LocalItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");
            }
        }
    }
}
