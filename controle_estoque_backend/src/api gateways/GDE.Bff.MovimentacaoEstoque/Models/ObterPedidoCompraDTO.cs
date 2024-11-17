namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class ObterPedidoCompraDTO
    {
        public Guid Id { get; set; }
        public Guid IdFuncionarioResponsavel { get; set; }
        public string? NomeFornecedor { get; set; }
        public int Numero { get; set; }
        public decimal PrecoTotal { get; set; }
        public string? DataCriacao { get; set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; set; }

    }
}
