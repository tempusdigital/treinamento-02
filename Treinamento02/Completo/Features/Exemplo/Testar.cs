using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Completo.Features.Exemplo
{
    public class Testar
    {
        public class Query : IRequest<string>
        {
            
        }

        public class Validar : AbstractValidator<string>
        {
            public Validar()
            {
                AdicionarValidacoes();
            }

            private void AdicionarValidacoes()
            {
                RuleFor(p => p)
                    .NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, string>
        {
            readonly Validar _validar;

            public QueryHandler(Validar validar)
            {
                _validar = validar;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                await _validar.ValidateAndThrowAsync("");
                return "Foi novamente";
            }
        }
    }
}
