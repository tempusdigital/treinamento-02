using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Tempus.Utils;

namespace EntityFramework.Services
{
    public class ProdutoService : IProdutoService
    {
        readonly LojaContext _lojaContext;
        readonly ValidarProduto _validarProduto;

        public ProdutoService(LojaContext lojaContext, ValidarProduto validarProduto)
        {
            _lojaContext = lojaContext;
            _validarProduto = validarProduto;
        }

        public async Task<int> InserirEditar(ProdutoInserirEditarDto produtoDto)
        {
            await _validarProduto.ValidateAndThrowAsync(produtoDto);

            Produto produto = null;

            if (produtoDto.Id == null)
            {
                produto = new Produto();
            
                _lojaContext.Add(produto);
            }
            else
            {
                produto = await _lojaContext
                    .Set<Produto>()
                    .Where(p => p.Id == produtoDto.Id)
                    .FirstOrDefaultAsync();

                ChecarSe.Encontrou(produto);
            }
            
            produto.Descricao = produtoDto.Descricao;
            produto.Valor = produtoDto.Valor;
            
            await _lojaContext.SaveChangesAsync();

            return produto.Id;
        }
        
        public async Task<ProdutoPesquisaResultadoDto[]> Listar(ProdutoPesquisaDto pesquisa)
        {
            var query = _lojaContext
                .Set<Produto>()
                .AsNoTracking()
                .AsQueryable();

            if (string.IsNullOrEmpty(pesquisa.Descricao) == false)
                query = query.Where(p => p.Descricao.StartsWith(pesquisa.Descricao));

            return await query
                .Select(p => new ProdutoPesquisaResultadoDto
                {
                    Id = p.Id,
                    Descricao = p.Descricao
                })
                .OrderBy(p => p.Descricao)
                .ToArrayAsync();
        }

        public async Task Excluir(int id)
        {
            var produto = await _lojaContext
                .Set<Produto>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            ChecarSe.Encontrou(produto);

            _lojaContext.Remove(produto);

            await _lojaContext.SaveChangesAsync();
        }
    }
}
