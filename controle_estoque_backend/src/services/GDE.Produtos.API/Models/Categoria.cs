using GDE.Core.Domain_Objects;

namespace GDE.Produtos.API.Models
{
    public class Categoria
    {
        public Categoria(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }
    }
}