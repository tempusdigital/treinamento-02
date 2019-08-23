using System;
using System.Collections.Generic;

namespace EntityFramework.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Telefones = new HashSet<ClienteTelefone>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CpfOuCnpj { get; set; }
        public int Tipo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public decimal Score { get; set; }

        public ICollection<ClienteTelefone> Telefones { get; set; }
    }
}
