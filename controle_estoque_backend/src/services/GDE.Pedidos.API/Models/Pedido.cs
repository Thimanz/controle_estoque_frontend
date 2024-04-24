using FluentValidation.Results;

namespace GDE.Pedidos.API.Models
{
    public abstract class Pedido
    {
        public Pedido(Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
        {
            Id = Guid.NewGuid();
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            PedidoItens = pedidoItens;

            CalcularPrecoTotal();
        }

        public Guid Id { get; private set; }
        public Guid IdFuncionarioResponsavel { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public List<PedidoItem> PedidoItens { get; private set; } = new List<PedidoItem>();
        
        public ValidationResult ValidationResult { get; private set; }

        private void CalcularPrecoTotal()
        {
            PrecoTotal = PedidoItens.Sum(i => i.PrecoUnitario * i.Quantidade);
        }
    }
}
