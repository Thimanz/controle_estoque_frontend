namespace GDE.Pedidos.API.DTO
{
    public class LocalDTO
    {
        public LocalDTO(Guid localId, string? localNome)
        {
            Id = localId;
            Nome = localNome;
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
    }
}
