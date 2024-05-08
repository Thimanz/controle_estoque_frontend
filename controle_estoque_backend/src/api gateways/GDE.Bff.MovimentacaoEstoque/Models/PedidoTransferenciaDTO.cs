namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class PedidoTransferenciaDTO
    {
        public Guid IdLocalDestino { get; set; }
        public Guid IdFuncionarioResponsavel { get; set; }
        public List<PedidoItemDTO> PedidoItens { get; set; }
    }
}
