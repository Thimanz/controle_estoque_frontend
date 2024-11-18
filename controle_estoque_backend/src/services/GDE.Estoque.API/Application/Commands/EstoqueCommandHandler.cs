using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.Estoque.API.Application.DTO;
using GDE.Estoque.Domain;
using MassTransit;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GDE.Estoque.API.Application.Commands
{
    public class EstoqueCommandHandler : CommandHandler,
        IRequestHandler<MovimentarItensEstoqueCommand, ValidationResult>
    {
        private readonly ILocalRepository _localRepository;
        private readonly IRequestClient<ProdutoMovimentadoIntegrationEvent> _requestClient;

        public EstoqueCommandHandler(ILocalRepository localRepository, 
            IRequestClient<ProdutoMovimentadoIntegrationEvent> requestClient)
        {
            _localRepository = localRepository;
            _requestClient = requestClient;
        }

        public async Task<ValidationResult> Handle(MovimentarItensEstoqueCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult!;

            foreach (var localId in message.LocalItens.Select(i => i.LocalId).Distinct())
            {
                var local = await _localRepository.ObterPorId(localId);

                if (local is null)
                {
                    AdicionarErro($"Local {localId} não encontrado");
                    return ValidationResult;
                }

                foreach (var item in message.LocalItens.Where(i => i.LocalId == local.Id))
                {
                    var localItem = MapearItem(item);

                    switch (message.Tipo)
                    {
                        case TipoMovimentacao.Entrada:
                            AdicionarItem(local, localItem);
                            break;
                        case TipoMovimentacao.Saida:
                            RemoverItem(local, localItem);
                            break;
                        case TipoMovimentacao.Transferencia:
                            await TransferirItem(local, localItem, message.IdLocalDestino!.Value);
                            break;
                    }

                    if (!ValidationResult.IsValid)
                        return ValidationResult;

                    var response = await AlterarQuantidadeEstoqueProduto(new ProdutoMovimentadoIntegrationEvent(item.ProdutoId, item.Quantidade, message.Tipo));

                    if(!response.ValidationResult.IsValid)
                        return response.ValidationResult;
                }
            }

            return await PersistirDados(_localRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> AlterarQuantidadeEstoqueProduto(ProdutoMovimentadoIntegrationEvent produtoMovimentado)
        {
            try
            {
                var response = await _requestClient.GetResponse<ResponseMessage>(produtoMovimentado);

                return response.Message;
            }
            catch
            {
                throw;
            }
        }

        private LocalItem MapearItem(LocalItemDTO itemDto)
        {
            return new LocalItem(
                    itemDto.LocalId,
                    itemDto.ProdutoId,
                    itemDto.NomeProduto,
                    new Dimensoes(itemDto.Comprimento, itemDto.Largura, itemDto.Altura),
                    itemDto.PrecoUnitario,
                    itemDto.Quantidade,
                    itemDto.DataValidade);
        }

        private void AdicionarItem(Local local, LocalItem item)
        {
            if (!local.VerificarEspacoLivre(item))
            {
                AdicionarErro($"Não há espaço suficiente no local {local.Nome} para adicionar o item {item.Nome}");
                return;
            }

            local.AdicionarItem(item);
            _localRepository.AdicionarItem(item);
        }

        private void RemoverItem(Local local, LocalItem item)
        {
            if (!local.ItemExistente(item))
            {
                AdicionarErro($"O produto {item.Nome} não foi encontrado em {local.Nome}");
                return;
            }

            var quantidadeAtual = local.ObterQuantidadePorProduto(item.ProdutoId);

            if (item.Quantidade > quantidadeAtual)
            {
                AdicionarErro("A quantidade informada é maior do que a existente no local");
                return;
            }
            var quantidadeARemover = item.Quantidade;

            foreach (var itemExistente in local.LocalItens.Where(i => i.ProdutoId == item.ProdutoId).Reverse())
            {
                if (itemExistente.Quantidade > quantidadeARemover)
                {
                    local.RemoverItem(itemExistente);
                    itemExistente.AdicionarQuantidadeItem(quantidadeARemover * -1);
                    local.AdicionarItem(itemExistente);
                    
                    _localRepository.AtualizarItem(itemExistente);

                    break;
                }
                else
                {
                    local.RemoverItem(itemExistente);
                    _localRepository.RemoverItem(itemExistente);

                    quantidadeARemover -= itemExistente.Quantidade;

                    if (quantidadeARemover <= 0)
                        break;
                }
            }
        }

        private async Task TransferirItem(Local localOrigem, LocalItem item, Guid IdLocalDestino)
        {
            var localDestino = await _localRepository.ObterPorId(IdLocalDestino);

            if (localDestino is null)
            {
                AdicionarErro($"Local {IdLocalDestino} não encontrado");
                return;
            }

            RemoverItem(localOrigem, item);
            AdicionarItem(localDestino, item);
        }
    }
}
