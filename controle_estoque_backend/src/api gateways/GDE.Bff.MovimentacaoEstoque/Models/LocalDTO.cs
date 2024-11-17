namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class LocalDTO
    {
        public LocalDTO(Guid id, string? nome, string? endereco, DimensoesDTO dimensoes, List<LocalItemDTO> localItens,
            DimensoesDTO espacoLivre, decimal espacoTotal, decimal espacoLivreCalculado)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Dimensoes = dimensoes;
            LocalItens = localItens;
            EspacoLivre = espacoLivre;
            EspacoTotal = espacoTotal;
            EspacoLivreCalculado = espacoLivreCalculado;
        }

        public Guid Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Endereco { get; private set; }
        public DimensoesDTO Dimensoes { get; private set; }
        public List<LocalItemDTO> LocalItens { get; private set; }
        public DimensoesDTO EspacoLivre { get; private set; }
        public decimal EspacoTotal { get; private set; }
        public decimal EspacoLivreCalculado { get; private set; }
    }
}