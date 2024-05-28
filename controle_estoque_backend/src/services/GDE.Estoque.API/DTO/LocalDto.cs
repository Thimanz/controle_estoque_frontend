using GDE.Estoque.Domain;

namespace GDE.Estoque.API.DTO
{
    public class LocalDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal EspacoTotal { get; set; }
        public decimal EspacoLivre { get; set; }
        public int QuantidadeItens { get; set; }
        
        public static LocalDto FromEntity(Local local) =>
            new LocalDto
            {
                Id = local.Id,
                Nome = local.Nome,
                EspacoTotal = local.EspacoTotal,
                EspacoLivre = local.EspacoLivreCalculado,
                QuantidadeItens = local.LocalItens.Sum(i => i.Quantidade)
            };
    }
}
