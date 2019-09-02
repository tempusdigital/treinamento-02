using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Completo.Features.Produto
{
    public class ProdutoController: Controller
    {
        readonly IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Listar(Listar.Query request)
        {
            var resultado = await _mediator.Send(request);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir([FromBody]InserirEditar.Command request)
        {
            var resultado = await _mediator.Send(request);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar([FromBody]InserirEditar.Command request)
        {
            var resultado = await _mediator.Send(request);
            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(Excluir.Command request)
        {
            var resultado = await _mediator.Send(request);
            return Json(resultado);
        }
    }
}
