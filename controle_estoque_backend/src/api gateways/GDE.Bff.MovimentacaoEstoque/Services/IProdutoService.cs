using GDE.Bff.MovimentacaoEstoque.Models;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> ObterProdutoPorId(Guid Id);
    }
}
