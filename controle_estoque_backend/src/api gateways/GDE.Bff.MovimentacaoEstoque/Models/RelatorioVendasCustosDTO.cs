namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class RelatorioVendasCustosDTO
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal TotalCompra { get; set; }
        public decimal TotalVenda { get; set; }
    }
}
