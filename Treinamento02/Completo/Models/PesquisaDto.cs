using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class PesquisaDto
    {
        public PesquisaOrdenacaoDto Ordenacao { get; set; }

        public string Nome { get; set; }

        public string CpfOuCnpj { get; set; }
    }

    public enum PesquisaOrdenacaoDto
    {
        Nome = 1,
        CpfOuCnpj = 2
    }
}
