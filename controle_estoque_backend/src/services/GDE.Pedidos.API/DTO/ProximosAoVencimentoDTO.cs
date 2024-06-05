namespace GDE.Pedidos.API.DTO
{
    public class ProximosAoVencimentoDTO
    {
        public ProximosAoVencimentoDTO(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
