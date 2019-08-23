using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Infra
{
    public static class ModelStateExtensions
    {
        public static string[] ObterErros(this ModelStateDictionary source)
        {
            return source
                .Where(s => s.Value.Errors.Any())
                .Select(s => $"{s.Key}: {string.Concat(s.Value.Errors.Select(e => e.ErrorMessage).ToArray())}")
                .ToArray();
        }
    }
}
