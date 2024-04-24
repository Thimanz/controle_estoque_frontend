using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Estoque.Domain;
using MediatR;

namespace GDE.Estoque.API.Application.Commands
{
    public class EstoqueCommandHandler : CommandHandler,
        IRequestHandler<AdicionarItensEstoqueCommand, ValidationResult>
    {
        private readonly ILocalRepository _localRepository;

        public EstoqueCommandHandler(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarItensEstoqueCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var local = await _localRepository.ObterPorId(message.LocalId);

            var itens = MapearItens(message);

            foreach (var item in itens)
            {
                if (!local.VerificarEspacoLivre(item))
                {
                    AdicionarErro("Não há espaço suficiente no local");
                    return message.ValidationResult;
                }

                local.AdicionarItem(item);
            }

            _localRepository.Atualizar(local);

            return await PersistirDados(_localRepository.UnitOfWork);
        }

        private List<Item> MapearItens(AdicionarItensEstoqueCommand message)
        {
            return message.Items
                .ConvertAll(i => new Item(
                    i.ProdutoId,
                    i.Nome,
                    new Dimensoes(i.Comprimento, i.Largura, i.Altura)));
        }
    }
}
