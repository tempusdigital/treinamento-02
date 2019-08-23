using DataBinding.Infra;
using EntityFramework.Models;
using EntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class ClienteController : Controller
    {
        readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Listar(PesquisaDto pesquisa)
        {
            var resultado = await _clienteService.Listar(pesquisa);

            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]ClienteDto clienteDto)
        {
            try
            {
                var id = await _clienteService.Inserir(clienteDto);
                return Json(id);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Campo, ex.Mensagem);
            }

            return BadRequest(ModelState.ObterErros());
        }

        [HttpPut]
        public async Task Editar([FromBody]ClienteDto clienteDto)
        {
            // ToDo: obter mensagens de validação
            await _clienteService.Editar(clienteDto);
        }

        [HttpDelete]
        public async Task Excluir(int id)
        {
            // ToDo: obter mensagens de validação
            await _clienteService.Excluir(id);
        }
    }
}