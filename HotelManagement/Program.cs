using System;
using HotelManagement.Servicos.QuartoService;
using HotelManagement.Servicos.ClienteService.Models;
using System.IO; 
namespace HotelManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Dados.Data.CarregarDados();
            ServicoQuarto.AtualizarSituacaoQuartos();
            bool programStatus = true;
            while (programStatus)
            {
                programStatus = Views.PrintTelaInicial();
            }
            //Views.PrintTelaQuartos("Livre",2);
            Dados.Data.SalvarDados();
        }
    }
}

