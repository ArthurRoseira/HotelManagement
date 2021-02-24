using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Entidades
{
    class Cliente
    {
        public string CPF { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento{ get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
