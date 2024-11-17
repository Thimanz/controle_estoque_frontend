using GDE.Bff.MovimentacaoEstoque.Models;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IEstoqueService
    {
        Task<LocalDTO> ObterLocalPorId(Guid Id);
    }
}