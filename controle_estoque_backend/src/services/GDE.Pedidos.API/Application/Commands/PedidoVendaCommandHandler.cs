﻿using FluentValidation.Results;
using GDE.Core.Messages;
using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Pedidos.API.Models;
using MediatR;

namespace GDE.Pedidos.API.Application.Commands
{
    public class PedidoVendaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarPedidoVendaCommand, ValidationResult>
    {
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private readonly IMessageBus _bus;

        public PedidoVendaCommandHandler(IPedidoVendaRepository pedidoVendaRepository, IMessageBus bus)
        {
            _pedidoVendaRepository = pedidoVendaRepository;
            _bus = bus;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoVendaCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var pedidoVenda = MapearItens(message);

            _pedidoVendaRepository.Adicionar(pedidoVenda);

            if (!message.ValidationResult.IsValid)
                return message.ValidationResult;

            var response = await RemoverItensEstoque(message);

            if(!response.ValidationResult.IsValid)
                return response.ValidationResult;

            return await PersistirDados(_pedidoVendaRepository.UnitOfWork);
        }

        private async Task<ResponseMessage> RemoverItensEstoque(AdicionarPedidoVendaCommand pedidoVenda)
        {
            var pedidoItemCadastrado = pedidoVenda.PedidoItens.ConvertAll(i => new PedidoItemIntegrationEvent
            (
                i.ProdutoId,
                i.LocalId,
                i.NomeProduto,
                i.Comprimento,
                i.Largura,
                i.Altura,
                i.Quantidade,
                i.PrecoUnitario,
                i.PedidoCompraId,
                i.PedidoVendaId,
                i.PedidoTransferenciaId)
            );


            var pedidoCadastrado = new PedidoCadastradoIntegrationEvent(TipoMovimentacao.Saida, pedidoItemCadastrado);

            try
            {
                return await _bus.RequestAsync<PedidoCadastradoIntegrationEvent, ResponseMessage>(pedidoCadastrado);
            }
            catch
            {
                throw;
            }
        }

        private PedidoVenda MapearItens(AdicionarPedidoVendaCommand message)
        {
            var itens = message.PedidoItens
                .ConvertAll(i => new PedidoItem(
                    i.ProdutoId,
                    i.LocalId,
                    i.Quantidade,
                    i.PrecoUnitario,
                    null,
                    i.PedidoVendaId,
                    null,
                    i.DataValidade));

            return new PedidoVenda(message.NomeCliente, message.Timestamp, message.IdFuncionarioResponsavel, itens);
        }
    }
}
