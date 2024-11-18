namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class LocalPedidoDTO
    {
        public LocalPedidoDTO(Guid id, string? nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; private set; }
        public string? Nome { get; set; }
    }
}