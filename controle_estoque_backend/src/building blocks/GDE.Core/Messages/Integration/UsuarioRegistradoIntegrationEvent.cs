using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDE.Core.Messages.Integration
{
    public class UsuarioRegistradoIntegrationEvent : IntegrationEvent 
    {
        public Guid Id { get; set; }
        public string? Nome { get; private set; }
        public string? Cpf { get; private set; }
        public string? Email { get; private set; }

        public UsuarioRegistradoIntegrationEvent(Guid id, string? nome, string? cpf, string? email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}
