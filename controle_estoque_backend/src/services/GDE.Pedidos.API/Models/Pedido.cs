using FluentValidation.Results;
using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public abstract class Pedido : Entity
    {
        protected Pedido(Guid idFuncionarioResponsavel,  DateTime dataCriacao, List<PedidoItem> pedidoItens)
        {
            IdFuncionarioResponsavel = idFuncionarioResponsavel;
            PedidoItens = pedidoItens;
            DataCriacao = dataCriacao;
            CalcularPrecoTotal();
        }

        protected Pedido() { }
        
        public Guid IdFuncionarioResponsavel { get; private set; }
        public int Numero { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<PedidoItem> PedidoItens { get; private set; } = new List<PedidoItem>();

        public ValidationResult ValidationResult { get; private set; }

        public int Quantidade()
        {
            return PedidoItens.Count;
        }

        private void CalcularPrecoTotal()
        {
            PrecoTotal = PedidoItens.Sum(i => i.PrecoUnitario * i.Quantidade);
        }
    }
}
