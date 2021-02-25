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
            Dados.Data.CarregarDados();
            //bool programStatus = true;
            //while(programStatus)
            //{
            //    programStatus = Views.PrintTelaInicial();
            //}
            Views.PrintTelaQuartos("Livre",1);
            Dados.Data.SalvarDadosClientes();
        }
    }
}
