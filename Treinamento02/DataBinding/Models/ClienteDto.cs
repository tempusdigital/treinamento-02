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
        [MinLength(3)]
        [MaxLength(50)]
        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Range(0, 10)]
        public decimal? Score { get; set; }
    }
}
