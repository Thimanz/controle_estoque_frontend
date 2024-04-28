using GDE.Estoque.Domain;

namespace GDE.Estoque.API.Application.DTO
{
    public class LocalItemDTO
    {
        public LocalItemDTO(Guid localId, Guid produtoId, string? nomeProduto, decimal comprimento, decimal largura, 
            decimal altura, decimal precoUnitario, int quantidade)
        {
            LocalId = localId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }

        public Guid LocalId { get; set; }
        public Guid ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
