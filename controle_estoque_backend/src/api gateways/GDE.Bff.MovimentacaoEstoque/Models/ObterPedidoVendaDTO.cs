namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class ObterPedidoVendaDTO
    {
        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public string? NomeCliente { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public string? DataCriacao { get; private set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; private set; } = new List<BuscarPedidoItemDTO>();
    }
}
