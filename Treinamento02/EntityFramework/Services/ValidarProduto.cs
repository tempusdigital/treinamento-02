using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Services
{
    public class ValidarProduto : AbstractValidator<ProdutoInserirEditarDto>
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

        private async Task<bool> SerUnico(ProdutoInserirEditarDto produto, string descricao, CancellationToken arg2)
        {
            return !await _lojaContext
                .Set<Produto>()
                .Where(p => p.Descricao == produto.Descricao && p.Id != produto.Id)
                .AnyAsync();
        }
    }
}
