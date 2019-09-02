using System;
using System.Collections.Generic;

namespace EntityFramework.Models
{
    public partial class ClienteTelefone
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int? ClienteId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
