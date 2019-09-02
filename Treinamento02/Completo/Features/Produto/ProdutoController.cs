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

        public async Task<IActionResult> Listar(Listar.Query query)
        {
            var resultado = await _mediator.Send(query);
            return Json(resultado);
        }
    }
}
