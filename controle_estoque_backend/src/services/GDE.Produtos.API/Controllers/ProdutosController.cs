﻿using GDE.Core.Controllers;
using GDE.Produtos.API.Data;
using GDE.Produtos.API.Entities;
using GDE.Produtos.API.Models.InputModels;
using GDE.Produtos.API.Models.ViewModels;
using GDE.Produtos.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDE.Produtos.API.Controllers
{
    //[Authorize]
    public class ProdutosController : MainController
    {
        private readonly ProdutoContext _context;
        //private readonly IUploadImagemRepository _imagemRepository;

        public ProdutosController(ProdutoContext context)
        {
            _context = context;
            //_imagemRepository = imagemRepository;
        }


        //[ClaimsAuthorize("Produto", "Ler")]
        [HttpGet("api/produto/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                return NotFound();

            var produtoViewModel = ProdutoViewModel.FromEntity(produto);




            return CustomResponse(produtoViewModel);
        }


        [HttpGet("api/produto/lista-por-nome/{nome}")]
        public async Task<IActionResult> ListaProdutosPorNome(string nome)
        {
            var produtos = await _context.Produtos.Include(p => p.Categoria).Where(p => p.Nome!.Contains(nome)).ToListAsync();

            return !produtos.Any() ? NotFound() : CustomResponse(produtos.Select(ProdutoViewModel.FromEntity));
        }


        [HttpGet("api/produto/categorias")]
        public IActionResult ListaCategorias()
        {
            var categorias = _context.Categorias.Select(CategoriaViewModel.FromEntity);

            return !categorias.Any() ? NotFound() : CustomResponse(categorias);
        }

        //[ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost("api/produto")]
        public async Task<IActionResult> AdicionarProduto(ProdutoInputModel produtoInputModel)
        {
            var produto = produtoInputModel.ToEntity();

            var categoria = await _context.Categorias.FindAsync(produto.CategoriaId);

            if (categoria is null)
            {
                AdicionarErroProcessamento("Categoria não encontrada");
                return CustomResponse();
            }

            //if (produtoInputModel.Imagem is not null)
            //    produto.Imagem = await _imagemRepository.UploadImagem(produtoInputModel.Imagem);

            ValidarProduto(produto);
            if (!OperacaoValida()) return CustomResponse();

            _context.Produtos.Add(produto);

            await PersistirDados();
            return CustomResponse();
        }

        //[ClaimsAuthorize("Produto", "Atualizar")]
        [HttpPut("api/produto/{produtoId}")]
        public async Task<IActionResult> AtualizarProduto(Guid produtoId, ProdutoInputModel produtoInputModel)
        {
            var produto = produtoInputModel.ToEntity();

            var produtoExistente = await _context.Produtos.FindAsync(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            ValidarProduto(produto);
            if (!OperacaoValida()) return CustomResponse();

            _context.Produtos.Update(produto);

            await PersistirDados();
            return CustomResponse();
        }

        //[ClaimsAuthorize("Produto", "Atualizar")]
        [HttpDelete("api/produto/{produtoId}")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {
            var produtoExistente = await _context.Produtos.FindAsync(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            await PersistirDados();
            _context.Remove(produtoExistente);

            return CustomResponse();
        }

        private bool ValidarProduto(Produto produto)
        {
            if (produto.IsValid()) return true;

            produto.ValidationResult!.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }

        private async Task PersistirDados()
        {
            var commited = await _context.Commit();
            if (!commited) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }
    }
}