using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.DTO
{
    public class ItemDTO
    {
        public Guid LocalId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }

    }
}
