namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class DimensoesDTO
    {
        public decimal Comprimento { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }

        public DimensoesDTO(decimal comprimento, decimal largura, decimal altura)
        {
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
        }
    }
}
