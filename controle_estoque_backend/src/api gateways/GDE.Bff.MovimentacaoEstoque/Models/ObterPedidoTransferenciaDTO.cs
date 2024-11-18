namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class ObterPedidoTransferenciaDTO
    {
        public ObterPedidoTransferenciaDTO(Guid id, Guid idFuncionarioResponsavel, LocalDTO? localDestino, int numero, decimal precoTotal, string? dataCriacao, List<BuscarPedidoItemDTO> pedidoItens)
        {
            Id = id;
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            LocalDestino = localDestino;
            Numero = numero;
            PrecoTotal = precoTotal;
            DataCriacao = dataCriacao;
            PedidoItens = pedidoItens;
        }

        public Guid Id { get; set; }
        public Guid IdFuncionarioResponsavel { get;  set; }
        public LocalDTO? LocalDestino { get; set; }
        public int Numero { get; set; }
        public decimal PrecoTotal { get; set; }
        public string? DataCriacao { get; set; }
        public List<BuscarPedidoItemDTO> PedidoItens { get; set; } = new List<BuscarPedidoItemDTO>();
    }
}
