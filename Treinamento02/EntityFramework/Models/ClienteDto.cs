using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CpfOuCnpj { get; set; }

        public DateTime DataNascimento { get; set; }

        public List<ClienteTelefoneDto> Telefones { get; set; }
    }

    public class ClienteTelefoneDto
    {
        public string Numero { get; set; }
    }
}
