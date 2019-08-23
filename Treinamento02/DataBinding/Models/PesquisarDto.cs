using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class PesquisarDto
    {
        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        public decimal Score { get; set; }
    }
}
