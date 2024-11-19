using GDE.Core.Controllers;
using GDE.Pedidos.API.Data;
using GDE.Pedidos.API.DTO.Relatorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDE.Pedidos.API.Controllers
{
    [Authorize]
    public class RelatorioController : MainController
    {
        private readonly PedidosContext _context;

        public RelatorioController(PedidosContext context)
        {
            _context = context;
        }

        [HttpGet("api/relatorios/vendas-custos/{qtdMeses}")]
        public async Task<IActionResult> ObterRelatorioVendasCustos(int qtdMeses)
        {
            var custos = await _context.PedidosCompra
                .Where(p => p.DataCriacao.Month >= (DateTime.Now.Month - qtdMeses))
                .GroupBy(p => new { p.DataCriacao.Year, p.DataCriacao.Month })
                .Select(g => new VendasCustosDTO
                {
                    Mes = g.Key.Month,
                    Ano = g.Key.Year,
                    TotalCompra = g.Sum(p => p.PrecoTotal),
                    TotalVenda = 0M
                })
                .ToListAsync();

            var vendas = await _context.PedidosVenda
                .Where(p => p.DataCriacao.Month >= (DateTime.Now.Month - qtdMeses))
                .GroupBy(p => new { p.DataCriacao.Year, p.DataCriacao.Month })
                .Select(g => new VendasCustosDTO
                {
                    Mes = g.Key.Month,
                    Ano = g.Key.Year,
                    TotalCompra = 0M,
                    TotalVenda = g.Sum(p => p.PrecoTotal)
                })
                .ToListAsync();

            var resultado = custos
                .Union(vendas)
                .GroupBy(x => new { x.Ano, x.Mes })
                .Select(g => new VendasCustosDTO
                {
                    Ano = g.Key.Ano,
                    Mes = g.Key.Mes,
                    TotalCompra = g.Sum(x => x.TotalCompra),
                    TotalVenda = g.Sum(x => x.TotalVenda)
                })
                .OrderBy(r => r.Ano).ThenBy(r => r.Mes)
                .ToList();


            if (resultado is null)
                return CustomResponse();

            return CustomResponse(resultado);
        }

        [HttpGet("api/relatorios/top10-produtos")]
        public async Task<IActionResult> ObterRelatorioTop10Produtos()
        {
            var resultado = await _context.PedidoItens
                .Where(p => p.PedidoVendaId.HasValue)
                .GroupBy(x => x.ProdutoId)
                .Select(g => new Top10ProdutosDTO
                {
                    ProdutoId = g.Key,
                    QuantidadeVendida = g.Sum(x => x.Quantidade)
                })
                .ToListAsync();

            if (resultado is null)
                return CustomResponse();

            return CustomResponse(resultado);
        }
    }
}
