using GDE.Estoque.Domain;

namespace GDE.Estoque.API.DTO
{
    public class AtualizarLocalDto
    {
        public string? Nome { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }


        public static Local ToEntity(AtualizarLocalDto dto, List<LocalItem> localItems) =>
            new Local(dto.Nome, dto.Altura, dto.Largura, dto.Comprimento, localItems);
    }
}
