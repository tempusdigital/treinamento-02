using DataBinding.Infra;
using DataBinding.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace DataBinding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult TestarQueryString(PesquisarClienteDto pesquisar)
        {
            return Ok($"Você enviou: CPF: {pesquisar.Cpf} - Data: {pesquisar.Data} - Score: {pesquisar.Score}");
        }

        [HttpPost]
        public IActionResult TestarForm(ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ObterErros());
            }

            return Ok($"Você enviou: Nome: {cliente.Nome} - Data de Nascimento: {cliente.DataNascimento} - Score: {cliente.Score}");
        }

        [HttpPost]
        public IActionResult TestarJson([FromBody]ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ObterErros());
            }

            return Ok($"Você enviou: Nome: {cliente.Nome} - Data de Nascimento: {cliente.DataNascimento} - Score: {cliente.Score}");
        }

        [HttpPost]
        public async Task<IActionResult> TestarArquivo(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                ModelState.AddModelError("arquivo", "Preencha o campo arquivo");

            if (arquivo != null && arquivo.Length > 1024 * 1024)
                ModelState.AddModelError("arquivo", "O arquivo deve ter no máximo 1 MB");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ObterErros());
            }

            var pasta = @"c:\treinamento";

            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var caminhoCompleto = Path.Combine(pasta, arquivo.FileName);

            using (var origem = arquivo.OpenReadStream())
            using (var destino = new FileStream(caminhoCompleto, FileMode.CreateNew))
            {
                await origem.CopyToAsync(destino);
            }

            return Ok($"Arquivo salvo com sucesso em: {caminhoCompleto}");
        }
    }
}
