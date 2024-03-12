namespace GDE.Identidade.API.Models.UserViewModels
{
    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }
}
