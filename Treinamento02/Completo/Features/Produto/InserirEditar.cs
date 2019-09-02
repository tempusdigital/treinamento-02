namespace Completo.Features.Produto
{
    using EntityFramework.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Tempus.Utils;

    public class InserirEditar
    {
        public class Command : IRequest<int>
        {
            public int? Id { get; set; }

            public string Descricao { get; set; }

            public decimal Valor { get; set; }
        }

        public class ValidarProduto: AbstractValidator<Command>
        {
            readonly LojaContext _lojaContext;

            public ValidarProduto(LojaContext lojaContext)
            {
                _lojaContext = lojaContext;

                CriarValidacao();
            }

            public void CriarValidacao()
            {
                RuleFor(p => p.Descricao)
                    .NotEmpty();

                RuleFor(p => p.Descricao)
                    .MustAsync(SerUnico)
                    .WithMessage("Produto já cadastrado");

                RuleFor(p => p.Valor)
                    .GreaterThan(0);
            }

            private async Task<bool> SerUnico(Command request, string descricao, CancellationToken arg2)
            {
                return !await _lojaContext
                    .Set<Produto>()
                    .Where(p => p.Descricao == request.Descricao && p.Id != request.Id)
                    .AnyAsync();
            }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            readonly LojaContext _lojaContext;
            readonly ValidarProduto _validar;

            public CommandHandler(LojaContext lojaContext, ValidarProduto validar)
            {
                _lojaContext = lojaContext;
                _validar = validar;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                await _validar.ValidateAndThrowAsync(request);

                Produto produto;

                if (request.Id == null)
                {
                    produto = new Produto();

                    _lojaContext.Add(produto);
                }
                else
                {
                    produto = await _lojaContext
                        .Set<Produto>()
                        .FirstOrDefaultAsync(p => p.Id == request.Id);

                    ChecarSe.Encontrou(produto);
                }

                Mapear(produto, request);

                await _lojaContext.SaveChangesAsync();

                return produto.Id;
            }

            private void Mapear(Produto produto, Command request)
            {
                produto.Descricao = request.Descricao;
                produto.Valor = request.Valor;
            }
        }
    }
}
