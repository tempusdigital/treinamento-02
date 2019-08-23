using DataBinding.Infra;
using DataBinding.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataBinding.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger _logger;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult TestarQueryString(PesquisarDto pesquisa)
        {
            return Ok($"Nome: {pesquisa.Nome}; Data Nascimento: {pesquisa.DataNascimento}; Score: {pesquisa.Score}");
        }

        [HttpPost]
        public IActionResult TestarForm(ClienteDto cliente)
        {
            /*
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome");

            if (cliente.Score < 0)
                ModelState.AddModelError("Score", "Preencha om um score positivo");

            if (ModelState.IsValid)
                return Ok($"Nome: {cliente.Nome}; Data Nascimento: {cliente.DataNascimento}; Score: {cliente.Score}");

            return BadRequest(ModelState.ObterErros());
            */

            if (ModelState.IsValid)
                return Ok($"Nome: {cliente.Nome}; Data Nascimento: {cliente.DataNascimento}; Score: {cliente.Score}");

            return BadRequest(ModelState.ObterErros());
        }

        [HttpPost]
        public IActionResult TestarJson([FromBody]ClienteDto cliente)
        {
            if (ModelState.IsValid)
                return Ok($"Nome: {cliente.Nome}; Data Nascimento: {cliente.DataNascimento}; Score: {cliente.Score}");

            return BadRequest(ModelState.ObterErros());
        }

        [HttpPost]
        public async Task<IActionResult> TestarArquivo(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                ModelState.AddModelError("arquivo", "Preencha o arquivo");

            if (arquivo != null && arquivo.Length > 1024 * 1024)
                ModelState.AddModelError("arquivo", "Arquivo deve ter no máximo 1 MB");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.ObterErros());

            var pasta = @"c:\treinamento";

            if (Directory.Exists(pasta) == false)
                Directory.CreateDirectory(pasta);

            var nomeCompleto = Path.Combine(pasta, arquivo.FileName);
            /*
            var origem = arquivo.OpenReadStream();
            try
            {
                var destino = new FileStream(nomeCompleto, FileMode.CreateNew);
                try
                {
                    await origem.CopyToAsync(destino);
                }
                finally
                {
                    destino.Dispose();
                }
            }
            finally
            {
                origem.Dispose();
            }
            */

            try
            {
                using (var origem = arquivo.OpenReadStream())
                using (var destino = new FileStream(nomeCompleto, FileMode.CreateNew))
                {
                    await origem.CopyToAsync(destino);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Falha ao salvar arquivo", ex);

                return BadRequest("Falha ao enviar o arquivo");
            }

            return Ok("Foi!");
        }
    }
}
