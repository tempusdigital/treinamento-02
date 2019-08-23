using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class PesquisarClienteDto
    {
        public string Cpf { get; set; }

        public DateTime? Data { get; set; }
        
        public decimal? Score { get; set; }
    }
}
