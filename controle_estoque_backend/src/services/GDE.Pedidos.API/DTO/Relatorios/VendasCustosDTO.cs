namespace GDE.Pedidos.API.DTO.Relatorios
{
    public class VendasCustosDTO
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal TotalCompra { get; set;}
        public decimal TotalVenda { get; set;}

    }
}
