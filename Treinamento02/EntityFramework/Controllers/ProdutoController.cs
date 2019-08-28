using EntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<int> Inserir([FromBody]ProdutoInserirEditarDto produto)
        {
            var id = await _produtoService.InserirEditar(produto);

            return id;
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