namespace GDE.Produtos.API.Entities
{
    public class Categoria
    {
        public Categoria(string? nome, string? descricao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }

        public List<Produto> Produtos { get; private set; } = new List<Produto>();
    }
}