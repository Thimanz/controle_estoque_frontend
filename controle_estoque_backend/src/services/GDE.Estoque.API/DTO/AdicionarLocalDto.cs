using GDE.Estoque.Domain;

namespace GDE.Estoque.API.DTO
{
    public class AdicionarLocalDto
    {
        public string? Nome { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public static Local ToEntity(AdicionarLocalDto dto) =>
            new Local(dto.Nome, dto.Altura, dto.Largura, dto.Comprimento, new List<LocalItem>());
    }
}
