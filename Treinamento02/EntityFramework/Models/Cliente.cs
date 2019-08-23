using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nome { get; set; }

        [MaxLength(15)]
        public string CpfOuCnpj { get; set; }

        public ClienteTipoEnum Tipo { get; set; }

        public DateTime DataNascimento { get; set; }

        [MaxLength(150)]
        public string Endereco { get; set; }

        public decimal Score { get; set; }
    }
}
