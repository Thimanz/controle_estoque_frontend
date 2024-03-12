using GDE.Core.Domain_Objects;

namespace GDE.Estoque.API.Models
{
    public class Categoria : Entity
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
    }
}