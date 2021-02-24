using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ClienteService;

namespace HotelManagement.Servicos.ClienteService.Models
{
    public class ModeloCadastro
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
            if(DataNascimento>DateTime.Now && Idade(DataNascimento)<18)
                throw new ClienteExceptions("Data de Nascimento Invalida");
        }

        public int Idade(DateTime dataNascimento)
        {
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (dataNascimento.Month > DateTime.Now.Month || (dataNascimento.Month == DateTime.Now.Month && dataNascimento.Day > DateTime.Now.Day))
                { idade -= 1; }

            return idade;
        }


    }
}
