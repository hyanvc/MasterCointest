using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ContatoTable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Celular { get; set; }

        public string Senha { get; set; }

        public DateTime DataDeNascimento { get; set; }

    }
}
