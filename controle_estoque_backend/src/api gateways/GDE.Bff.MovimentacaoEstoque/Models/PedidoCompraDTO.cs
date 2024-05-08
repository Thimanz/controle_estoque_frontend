namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class PedidoCompraDTO
    {
        public string? NomeFornecedor { get; set; }
        public Guid IdFuncionarioResponsavel { get; set; }
        public List<PedidoItemDTO> PedidoItens { get; set; }

    }
}
