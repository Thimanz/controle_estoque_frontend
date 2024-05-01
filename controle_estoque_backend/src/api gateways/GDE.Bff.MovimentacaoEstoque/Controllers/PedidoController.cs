using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using GDE.Core.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
    [Authorize]
    public class PedidoController : MainController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProdutoService _produtoService;
        private readonly IAspNetUser _user;

        public PedidoController(IPedidoService pedidoService, IProdutoService produtoService, IAspNetUser user)
        {
            _pedidoService = pedidoService;
            _produtoService = produtoService;
            _user = user;
        }

        [HttpPost("api/movimentacao/pedido/compra")]
        public async Task<IActionResult> AdicionarPedido(PedidoCompraDTO pedidoCompraDTO)
        {
            pedidoCompraDTO.IdFuncionarioResponsavel = _user.ObterUserId();

            foreach (var item in pedidoCompraDTO.PedidoItens)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.ProdutoId);

                item.NomeProduto = produto.Nome;
                item.Comprimento = produto.Comprimento;
                item.Largura = produto.Largura;
                item.Altura = produto.Altura;
            }

            var response = await _pedidoService.AdicionarPedidoCompra(pedidoCompraDTO);

            return CustomResponse(response);
        }
    }
}
