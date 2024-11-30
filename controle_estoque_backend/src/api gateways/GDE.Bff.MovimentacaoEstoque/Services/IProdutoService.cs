using GDE.Bff.MovimentacaoEstoque.Models;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<NotificacaoDTO>> ObterNotificacoesEstoqueBaixo();
        Task<ProdutoDTO> ObterProdutoPorId(Guid Id);
    }
}
