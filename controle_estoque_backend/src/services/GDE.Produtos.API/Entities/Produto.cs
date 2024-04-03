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
            decimal comprimento, decimal largura, decimal altura, decimal peso)
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
            Dimensoes = new Dimensoes(comprimento, largura, altura, peso);
            Ativo = true;
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
        public Guid CategoriaId { get; set; }
        public string? CodigoBarras { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? Imagem { get; set; }
        //public int QuantidadeEstoque { get; private set; }
        public int NivelMinimoEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Dimensoes? Dimensoes { get; set; }

        [JsonIgnore]
        public Categoria Categoria { get; set; }

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

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
