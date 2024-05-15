namespace GDE.Produtos.API.Models.ViewModels
{
    public class ProdutosEstoqueBaixoViewModel
    {
        public ProdutosEstoqueBaixoViewModel(Guid id, string? nome, string? mensagem)
        {
            Id = id;
            Nome = nome;
            Mensagem = mensagem;
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Mensagem { get; set; }
    }
}
