using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ClienteService;
using HotelManagement.Servicos.ReservaService.Models;
using HotelManagement.Servicos.QuartoService;
using HotelManagement.Servicos.ClienteService.Models;

namespace HotelManagement
{
    class Views
    {
        public static bool PrintTelaInicial()
        {
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Selecione a Operação Desejada:");
            Console.WriteLine("1 - Iniciar Nova Reserva;");
            Console.WriteLine("2 - Realizar CheckIn");
            Console.WriteLine("3 - Realizar CheckOut");
            Console.WriteLine("4 - Consultar Clientes;");
            Console.WriteLine("5 - Consultar Reserva;");
            Console.WriteLine("6 - Consultar Disponibilidade de Quartos;");
            Console.WriteLine("7 - Encerrar");
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    PrintTelaNovaReserva();
                    return true;
                case "2":
                    return true;
                case "3":
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        public static void PrintTelaNovaReserva()
        {
            var cliente = new ConsultaCPF();

            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Realizar Nova Reserva - Selecionar Cliente:");
            Console.WriteLine("Insira um CPF Válido:");
            string cpf = Console.ReadLine();
            cliente = ServicoCliente.ObterPorCPF(cpf);
            if (cliente != null)
            {
                Console.Clear();
                Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                Console.WriteLine();
                Console.WriteLine("Cliente Ja Cadastrado:");
                Console.WriteLine($"CPF: {cliente.CPF}");
                Console.WriteLine($"Nome Completo: {cliente.NomeCompleto}");
                //TODO: Modificar Para Idade
                Console.WriteLine($"Data De Nacimento: {cliente.DataNascimento}");
                Console.WriteLine($"Telefone: {cliente.Telefone}");
                Console.WriteLine($"Email: {cliente.Email}");
                Console.WriteLine();
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("CPF Não encontrado Redirecionando Para Novo Cadastro");
                PrintTelaCadastroCliente(cpf);
            }
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Realizar Nova Reserva - Selecionar Quarto:");
            var novaReserva = new CadastrarNova();
            novaReserva.CPF = cpf;
            Console.WriteLine("Selecione Um quarto Tipo de Quarto: ");
            //PrintTelaQuartos()
            Console.WriteLine("Dados Da Reserva: ");
            Console.WriteLine("Dados Da Reserva: ");
        }

        public static void PrintTelaCadastroCliente(string cpf) 
        {
            var novoCliente = new CadastrarNovo();
            novoCliente.CPF = cpf;
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.Write($"CPF: {cpf}");
            Console.Write("Nome Completo: ");
            novoCliente.NomeCompleto = Console.ReadLine();
            Console.Write("Data de Nascimento (dd/MM/yyyy): ");
            novoCliente.DataNascimento = DateTime.Parse(Console.ReadLine());
            Console.Write("Telefone: ");
            novoCliente.Telefone = Console.ReadLine();
            Console.Write("Email: ");
            novoCliente.Email = Console.ReadLine();
            ServicoCliente.CadastrarNovoCliente(novoCliente);
        }

       public static void PrintTelaQuartos(string status, int tipo)
        {
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Quartos Disponivéis:");
            var listaQuartos = ServicoQuarto.BuscarPorStatus(status, tipo);
            listaQuartos.ForEach(q =>
            {
                Console.WriteLine($"{q.QuartoId}, Situação:{ServicoQuarto.ObterSituação(q.SituacaoId).Descricao}");
            });
        }



    }
}
