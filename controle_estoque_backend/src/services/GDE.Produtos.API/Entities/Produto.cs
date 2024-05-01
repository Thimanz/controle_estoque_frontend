using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace GDE.Produtos.API.Entities
{
    public class Produto
    {
        public Produto() { }

        public Produto(string? nome, string? descricao, string? codigoBarras, Guid categoriaId,
            decimal precoCusto, decimal precoVenda, int nivelMinimoEstoque,
            decimal comprimento, decimal largura, decimal altura)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            CodigoBarras = codigoBarras;
            CategoriaId = categoriaId;
            PrecoCusto = precoCusto;
            PrecoVenda = precoVenda;
            NivelMinimoEstoque = nivelMinimoEstoque;
            DataCadastro = DateTime.Now;
            Dimensoes = new Dimensoes(comprimento, largura, altura);
            Ativo = true;
        }

        public Guid Id { get; set; }
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string? CodigoBarras { get; private set; }
        public decimal PrecoCusto { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public string? Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public int NivelMinimoEstoque { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public Dimensoes? Dimensoes { get; private set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public void RetirarEstoque(int quantidade)
        {
            QuantidadeEstoque -= quantidade;
        }

        public void AdicionarEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool EstaDisponivel(int quantidade)
        {
            return Ativo && QuantidadeEstoque >= quantidade;
        }

        public bool IsValid()
        {
            var erros = new AdicionarProdutoValidation().Validate(this).Errors;
            ValidationResult = new ValidationResult(erros);

            return ValidationResult.IsValid;
        }

        public class AdicionarProdutoValidation : AbstractValidator<Produto>
        {
            public AdicionarProdutoValidation()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Descricao)
                    .NotEmpty()
                    .WithMessage("A descrição do produto não foi informada");

                RuleFor(c => c.CodigoBarras)
                    .NotEmpty()
                    .WithMessage("O código de barras do produto não foi informado");

                RuleFor(c => c.CategoriaId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("A categoria do produto não foi informada");

                //RuleFor(c => c.Categoria!.Nome)
                //    .NotNull()
                //    .WithMessage("O nome da categoria não foi informado");

                //RuleFor(c => c.Categoria!.Descricao)
                //    .NotNull()
                //    .WithMessage("A descrição da categoria não foi informada");

                RuleFor(c => c.PrecoCusto)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Preço de custo do produto inválido");

                RuleFor(c => c.PrecoVenda)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Preço de venda do produto inválido");
            }
        }
    }
}
