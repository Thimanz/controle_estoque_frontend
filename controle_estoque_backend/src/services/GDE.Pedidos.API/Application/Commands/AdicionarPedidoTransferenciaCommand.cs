using FluentValidation;
using GDE.Core.Messages;
using GDE.Pedidos.API.Application.DTO;

namespace GDE.Pedidos.API.Application.Commands
{
    public class AdicionarPedidoTransferenciaCommand : Command
    {
        public AdicionarPedidoTransferenciaCommand(Guid idFuncionarioResponsavel, List<PedidoItemDTO> pedidoItens, Guid? idLocalDestino = null)
        {
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            PedidoItens = pedidoItens;
            IdLocalDestino = idLocalDestino;
        }

        public Guid IdFuncionarioResponsavel { get; set; }
        public List<PedidoItemDTO> PedidoItens { get; private set; }
        public Guid? IdLocalDestino { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarPedidoTransferenciaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarPedidoTransferenciaValidation : AbstractValidator<AdicionarPedidoTransferenciaCommand>
        {
            public AdicionarPedidoTransferenciaValidation()
            {
                RuleFor(c => c.IdLocalDestino)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Local de Destino não informado");

                RuleFor(c => c.IdFuncionarioResponsavel)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do Funcionário Responsável não informado");

                RuleFor(c => c.PedidoItens.Any())
                    .Equal(true)
                    .WithMessage("Nenhum item informado");
            }
        }
    }
}
