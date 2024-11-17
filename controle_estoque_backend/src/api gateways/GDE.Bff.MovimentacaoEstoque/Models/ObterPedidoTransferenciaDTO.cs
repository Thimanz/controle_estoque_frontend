namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class ObterPedidoTransferenciaDTO
    {
        public Guid Id { get; set; }
        public Guid IdFuncionarioResponsavel { get;  set; }
        public LocalDTO? LocalDestino { get; set; }
        public int Numero { get; set; }
        public decimal PrecoTotal { get; set; }
        public string? DataCriacao { get; set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; set; } = new List<BuscarPedidoItemDTO>();
    }
}
