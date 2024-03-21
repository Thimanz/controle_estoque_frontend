using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using GDE.Core.Domain_Objects;

namespace GDE.Produtos.API.Models
{
    public class Produto : IAggregateRoot
    {
        public Produto() { }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
        public string? CodigoBarras { get; set; }
        public Categoria? Categoria { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public string? Imagem { get; set; }
        //public int QuantidadeEstoque { get; private set; }
        public int NivelMinimoEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Dimensoes Dimensoes { get; set; }

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }


        internal bool IsValid()
        {
            var erros = new ProdutoValidation().Validate(this).Errors;
            ValidationResult = new ValidationResult(erros);

            return ValidationResult.IsValid;
        }

        public class ProdutoValidation: AbstractValidator<Produto>
        {
            public ProdutoValidation() 
            {
                RuleFor(p => p.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Descricao)
                    .NotEmpty()
                    .WithMessage("A descrição do produto não foi informada");

                RuleFor(c => c.CodigoBarras)
                    .NotEmpty()
                    .WithMessage("O código de barras do produto não foi informado");

                RuleFor(c => c.Categoria)
                    .NotNull()
                    .WithMessage("A categoria do produto não foi informada");

                RuleFor(c => c.Categoria!.Nome)
                    .NotNull()
                    .WithMessage("O nome da categoria não foi informado");

                RuleFor(c => c.Categoria!.Descricao)
                    .NotNull()
                    .WithMessage("A descrição da categoria não foi informada");

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
