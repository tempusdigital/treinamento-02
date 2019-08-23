using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Completo.Features.Exemplo
{
    public class ExemploController : Controller
    {
        readonly IMediator _mediator;

        public ExemploController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Listar()
        {
            return await _mediator.Send(new Testar.Query());
        }
    }
}