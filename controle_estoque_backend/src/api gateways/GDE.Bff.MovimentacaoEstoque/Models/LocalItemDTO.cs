namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class LocalItemDTO
    {
        public LocalItemDTO(Guid id, Guid localId, Guid produtoId, string? nome, DimensoesDTO dimensoes, decimal preco, int quantidade, string? imagem)
        {
            Id = id;
            LocalId = localId;
            ProdutoId = produtoId;
            Nome = nome;
            Dimensoes = dimensoes;
            Preco = preco;
            Quantidade = quantidade;
            Imagem = imagem;
        }

        public Guid Id { get; private set; }
        public Guid LocalId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string? Nome { get; private set; }
        public DimensoesDTO Dimensoes { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }
        public string? Imagem { get; set; }

    }
}