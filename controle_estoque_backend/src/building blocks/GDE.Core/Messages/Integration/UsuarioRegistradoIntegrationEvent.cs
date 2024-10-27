namespace GDE.Core.Messages.Integration
{
    public class UsuarioRegistradoIntegrationEvent : IntegrationEvent 
    {
        public static string QueueName => "gde.identidade.usuario.registrado";

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }

        public UsuarioRegistradoIntegrationEvent() { }

        public UsuarioRegistradoIntegrationEvent(Guid id, string? nome, string? cpf, string? email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}
