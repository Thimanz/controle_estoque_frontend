namespace GDE.Produtos.API.Repositories
{
    public interface IUploadImagemRepository
    {
        public Task<string> UploadImagem(IFormFile imagem);
        byte[] GetImagem(string imagem);
    }
}
