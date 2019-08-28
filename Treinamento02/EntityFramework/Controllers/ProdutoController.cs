using DataBinding.Infra;
using EntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class ProdutoController : Controller
    {
        readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Listar(ProdutoPesquisaDto pesquisa)
        {
            var resultado = await _produtoService.Listar(pesquisa);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]ProdutoInserirEditarDto produto)
        {
            try
            {
                var id = await _produtoService.InserirEditar(produto);

                return Json(id);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Campo, ex.Mensagem);
            }

            return Json(ModelState.ObterErros());
        }

        [HttpPut]
        public async Task<int> Editar([FromBody]ProdutoInserirEditarDto produto)
        {
            var id = await _produtoService.InserirEditar(produto);
            return id;
        }

        [HttpDelete]
        public async Task Excluir(int id)
        {
            await _produtoService.Excluir(id);
        }
    }
}