using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Servicos.ClienteService.Models
{
    class ConsultaCPF
    {
        public string CPF { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
    }
}
