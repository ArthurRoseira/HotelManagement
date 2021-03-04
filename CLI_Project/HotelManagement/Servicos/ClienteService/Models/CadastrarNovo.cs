using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ClienteService;

namespace HotelManagement.Servicos.ClienteService.Models
{
    public class CadastrarNovo
    {

        public string CPF { get; set; }
        public string NomeCompleto { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }


        public void Validar()
        {
            if (CPF.Length < 11)
                throw new ClienteExceptions("CPF Invalido");
            if (NomeCompleto.Split(" ").Length<2)
                throw new ClienteExceptions("Nome não esta Completo");
            if (Telefone.Length < 11)
                throw new ClienteExceptions("Numero de Telefone Invalido");
            if(DataNascimento>DateTime.Now && ClienteService.ServicoCliente.Idade(DataNascimento)<18)
                throw new ClienteExceptions("Data de Nascimento Invalida");
        }



    }
}
