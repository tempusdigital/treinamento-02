namespace Completo.Features.Produto
{
    using EntityFramework.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class Listar
    {
        public class Query : IRequest<ProdutoDto[]>
        {
            public string Pesquisa { get; set; }
        }

        public class ProdutoDto
        {
            public int Id { get; set; }

            public string Descricao { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, ProdutoDto[]>
        {
            readonly LojaContext _lojaContext;

            public QueryHandler(LojaContext lojaContext)
            {
                _lojaContext = lojaContext;
            }

            public async Task<ProdutoDto[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _lojaContext
                    .Set<Produto>()
                    .AsTracking()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Pesquisa))
                    query = query.Where(p => p.Descricao.StartsWith(request.Pesquisa));

                return await query
                    .Select(p => new ProdutoDto
                    {
                        Id = p.Id,
                        Descricao = p.Descricao
                    })
                    .OrderBy(p => p.Descricao)
                    .ToArrayAsync();
            }
        }
    }
}
