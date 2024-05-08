using FluentValidation;
using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.Estoque.API.Application.DTO;
using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.Commands
{
    public class MovimentarItensEstoqueCommand : Command
    {
        public TipoMovimentacao Tipo { get; set; }
        public List<LocalItemDTO> LocalItens { get; private set; }
        public Guid? IdLocalDestino { get; private set; }

        public MovimentarItensEstoqueCommand(TipoMovimentacao tipo, List<LocalItemDTO> localItens, Guid? idNovoDestino = null)
        {
            Tipo = tipo;
            LocalItens = localItens;
            IdLocalDestino = idNovoDestino;
        }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarItensEstoqueValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarItensEstoqueValidation : AbstractValidator<MovimentarItensEstoqueCommand>
        {
            public AdicionarItensEstoqueValidation()
            {
                RuleFor(c => c.LocalItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");

                RuleFor(c => c.IdLocalDestino)
                    .NotEqual(Guid.Empty)
                    .When(c => c.Tipo == TipoMovimentacao.Transferencia)
                    .WithMessage("Id do Local de Destino não informado");

            }
        }
    }
}
