using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBinding.Models
{
    public class ClienteDto
    {
        [Required]
        [MaxLength(15)]
        public string Nome { get; set; }

        [Required]
        public DateTime? DataNascimento { get; set; }

        public decimal Score { get; set; }
    }
}
