using System;
using HotelManagement.Servicos.ClienteService;
using HotelManagement.Servicos.ClienteService.Models;
using System.IO; 
namespace HotelManagement
{
    class Program
    {
        static void Main(string[] args)
        {

            var c = new ModeloCadastro()
            {
                CPF = "08876543",
                NomeCompleto = "Arthur Santos",
                DataNascimento = new DateTime(1996,01,15),
                Email = "arthur@gmail.com",
                Telefone = "041992119904"
            };


            var c2 = new ModeloCadastro()
            {
                CPF = "08823423423",
                NomeCompleto = "Carlos Alberto",
                DataNascimento = new DateTime(1998,10,10),
                Email = "arthur@gmail.com",
                Telefone = "041992119904"
            };


            ServicoCliente.CadastrarNovo(c);
            ServicoCliente.CadastrarNovo(c2);
            var lista = ServicoCliente.Buscar();

            lista.ForEach(c =>
            {
                Console.WriteLine(c.NomeCompleto);
                Console.WriteLine(c.Telefone);
            });


        }
    }
}
