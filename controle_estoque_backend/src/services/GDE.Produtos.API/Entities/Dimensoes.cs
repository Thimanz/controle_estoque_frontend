namespace GDE.Produtos.API.Entities
{
    public class Dimensoes
    {
        public decimal Comprimento { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }

        //Construtor do EntityFramework
        public Dimensoes() { }

        public Dimensoes(decimal comprimento, decimal largura, decimal altura)
        {
            Comprimento = comprimento;
            Largura = largura;
            Altura = altura;
        }
    }
}
