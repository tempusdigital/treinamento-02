using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Completo.Features.Cliente
{
    public class ClienteController : Controller
    {
        readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Listar()
        {
            return await _mediator.Send(new Testar.Query());
        }
    }
}