namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class PedidoVendaDTO
    {
        public string? NomeCliente { get; set; }
        public Guid IdFuncionarioResponsavel { get; set; }
        public List<PedidoItemDTO> PedidoItens { get; set; }
    }
}
