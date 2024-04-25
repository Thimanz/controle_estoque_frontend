using FluentValidation.Results;
using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public abstract class Pedido : Entity
    {
        protected Pedido(Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
        {
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            PedidoItens = pedidoItens;

            CalcularPrecoTotal();
        }

        protected Pedido() { }
        
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
