namespace Completo.Features.Produto
{
    using EntityFramework.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Tempus.Utils;

    public class Excluir
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            readonly LojaContext _lojaContext;

            public CommandHandler(LojaContext lojaContext)
            {
                _lojaContext = lojaContext;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var produto = await _lojaContext
                    .Set<Produto>()
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                ChecarSe.Encontrou(produto);

                _lojaContext.Remove(produto);

                await _lojaContext.SaveChangesAsync();
            }
        }
    }
}
