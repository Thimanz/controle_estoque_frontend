using GDE.Core.Domain_Objects;
using GDE.Produtos.API.Repositories;

namespace GDE.Produtos.API.Repositories
{
    public class UploadImageRepository : IUploadImagemRepository
    {
        private IWebHostEnvironment _environment;
        private const string pastaImagens = "Uploads";

        public UploadImageRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadImagem(IFormFile imagem)
        {
            var caminhoArquivos = _environment.ContentRootPath;
            var caminho = Path.Combine(caminhoArquivos, pastaImagens);

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            var ext = Path.GetExtension(imagem.FileName);
            var extensoesAceitas = new string[] { ".jpg", ".png", ".jpeg" };

            if (!extensoesAceitas.Contains(ext))
                throw new DomainException("Extensão não suportada.");

            var novoNome = string.Concat(Guid.NewGuid().ToString(), ext);

            var caminhoFinal = Path.Combine(caminho, novoNome);

            using (FileStream fs = File.Create(caminhoFinal))
            {
                await imagem.CopyToAsync(fs);
                fs.Flush();
            }

            return novoNome;
        }

        public byte[] GetImagem(string imagem)
        {
            var caminhoArquivos = _environment.ContentRootPath;
            var caminho = Path.Combine(caminhoArquivos, pastaImagens, imagem);

            var bytes = File.ReadAllBytes(caminho);

            return bytes;
        }
    }
}
