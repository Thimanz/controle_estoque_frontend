using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.DTO
{
    public class LocalItemDTO
    {
        public LocalItemDTO(Guid localId, Guid produtoId, decimal preco, int quantidade)
        {
            LocalId = localId;
            ProdutoId = produtoId;
            Preco = preco;
            Quantidade = quantidade;
        }

        public Guid LocalId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? Nome { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
