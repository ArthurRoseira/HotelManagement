using System;
using System.Collections.Generic;
using System.IO;
using HotelManagement.Entidades;
namespace HotelManagement.Dados
{
    class Data
    {
        public static List<Cliente> ListaClientes = new List<Cliente>();
        public static List<Reserva> ListaReservas = new List<Reserva>();
        public string DadosLocal = "C:\\Users\\arthur.santos\\Documents\\Curso C# Basico\\HotelManagement\\HotelManagement\\Dados";
        private string[] Tabelas = { "Clientes", "Reserva" };

        public void CarregarDados()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv",SearchOption.TopDirectoryOnly);
            //for(if=)

        }
    }
}
