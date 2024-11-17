﻿namespace GDE.Bff.MovimentacaoEstoque.Models
{
    public class NotificacaoDTO
    {
        public NotificacaoDTO() { }
        
        public NotificacaoDTO(Guid id, string? nome, string? mensagem)
        {
            Id = id;
            Nome = nome;
            Mensagem = mensagem;
        }

        public NotificacaoDTO(Guid id, TipoNotificacao tipo, string? nome, string? mensagem)
        {
            Id = id;
            Tipo = tipo;
            Nome = nome;
            Mensagem = mensagem;
        }

        public Guid Id { get; set; }
        public TipoNotificacao Tipo { get; set; }
        public string? Nome { get; set; }
        public string? Mensagem { get; set; }
    }
}
