using DataBinding.Infra;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class ClienteOldController : Controller
    {
        readonly LojaContext _lojaContext;

        public ClienteOldController(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }

        public async Task<IActionResult> Listar(PesquisaDto pesquisa)
        {
            var query = _lojaContext
                .Set<Cliente>()
                .AsQueryable();

            if (pesquisa.Nome != null && pesquisa.CpfOuCnpj != null)
                query = query.Where(p => p.Nome == pesquisa.Nome || p.CpfOuCnpj == pesquisa.CpfOuCnpj);
            else
            if (pesquisa.Nome != null)
                query = query.Where(p => p.Nome == pesquisa.Nome);
            else
            if (pesquisa.CpfOuCnpj != null)
                query = query.Where(p => p.CpfOuCnpj == pesquisa.CpfOuCnpj);

            switch (pesquisa.Ordenacao)
            {
                case PesquisaOrdenacaoDto.Nome:
                    query = query.OrderBy(p => p.Nome);
                    break;

                case PesquisaOrdenacaoDto.CpfOuCnpj:
                    query = query.OrderBy(p => p.CpfOuCnpj);
                    break;
            }

            var resultado = await query
                .Select(p => new
                {
                    p.Id,
                    p.Nome
                })
                .ToArrayAsync();

            return Json(resultado);
        }

        public async Task<IActionResult> ListarSemTelefone()
        {
            var resultado = await _lojaContext
                .Set<Cliente>()
                .Where(c => c.Telefones.Any() == false)
                .ToArrayAsync();

            return Json(resultado);
        }

        public async Task<IActionResult> ObterUltimoTelefone()
        {
            var resultado = await _lojaContext
                .Set<Cliente>()
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                    UltimoTelefone = c.Telefones
                        .OrderBy(t => t.Id)
                        .Select(s => s.Numero)
                        .LastOrDefault()
                })
                .ToArrayAsync();

            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]ClienteDto clienteDto)
        {
            if (clienteDto.Nome == null)
                ModelState.AddModelError("Nome", "Preencha o nome");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.ObterErros());

            //cliente.Id = GerarProximoId();
            var cliente = new Cliente();

            cliente.Nome = clienteDto.Nome.ToUpper();
            cliente.CpfOuCnpj = clienteDto.CpfOuCnpj.Trim();
            cliente.DataNascimento = clienteDto.DataNascimento;

            if (clienteDto.Telefones != null)
            {
                foreach (var telefoneDto in clienteDto.Telefones)
                {
                    var telefone = new ClienteTelefone();

                    telefone.Numero = telefoneDto.Numero;

                    cliente.Telefones.Add(telefone);
                }
            }

            _lojaContext.Add(cliente);

            await _lojaContext.SaveChangesAsync();

            return Json(cliente.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody]ClienteDto clienteDto)
        {
            if (clienteDto.Nome == null)
                ModelState.AddModelError("Nome", "Preencha o nome");

            if (ModelState.IsValid == false)
                return BadRequest(ModelState.ObterErros());

            var cliente = await _lojaContext
                .Set<Cliente>()
                .Where( p=> p.Id == clienteDto.Id)
                .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound();

            cliente.Nome = clienteDto.Nome.ToUpper();
            cliente.CpfOuCnpj = clienteDto.CpfOuCnpj.Trim();
            cliente.DataNascimento = clienteDto.DataNascimento;

            await _lojaContext.SaveChangesAsync();

            return Ok("Salvo!");
        }

        [HttpDelete]
        public async Task<IActionResult> Excluir(int id)
        {
            var cliente = await _lojaContext
                .Set<Cliente>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound();

            _lojaContext.Remove(cliente);

            await _lojaContext.SaveChangesAsync();

            return Ok("Removido!");
        }
    }
}