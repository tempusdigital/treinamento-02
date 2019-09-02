using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class ClienteComTelefoneQuery
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CpfOuCnpj { get; set; }
        public int Tipo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public decimal Score { get; set; }
        public string Telefone { get; set; }
    }
}
