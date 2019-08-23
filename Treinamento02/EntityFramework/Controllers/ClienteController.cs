using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Controllers
{
    public class ClienteController : Controller
    {
        readonly LojaContext _lojaContext;

        public ClienteController(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }

        public async Task<IActionResult> Listar([FromBody]Cliente cliente)
        {
            /*
            var clientes = await _lojaContext
                .Set<Cliente>()
                .Where(p => p.Id == 4)
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Telefones
                })
                .FirstOrDefaultAsync();
            */

            var clientes = await _lojaContext
                .Set<Cliente>()
                .Where(p => p.Id == 4)
                .FirstOrDefaultAsync();

            return Json(clientes);
        }
    }
}