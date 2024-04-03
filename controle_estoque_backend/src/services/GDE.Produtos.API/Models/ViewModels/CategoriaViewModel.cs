using GDE.Produtos.API.Entities;

namespace GDE.Produtos.API.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel(Guid id, string? nome, string? descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }

        public Guid Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }

        public static CategoriaViewModel FromEntity(Categoria categoria) =>
            new CategoriaViewModel(categoria.Id, categoria.Nome, categoria.Descricao);
    }
}
