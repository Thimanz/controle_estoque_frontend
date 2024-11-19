namespace GDE.Estoque.API.DTO
{
    public class ProximosAoVencimentoDTO
    {
        public ProximosAoVencimentoDTO(Guid idProduto, Guid idLocal)
        {
            IdProduto = idProduto;
            IdLocal = idLocal;
        }

        public Guid IdProduto { get; set; }
        public Guid IdLocal { get; set; }
    }
}
