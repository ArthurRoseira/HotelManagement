using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ClienteService;
using HotelManagement.Servicos.ReservaService;
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
            Console.WriteLine("2 - Alterar Dados de Reserva");
            Console.WriteLine("3 - Consultar Clientes;");
            Console.WriteLine("4 - Consultar todas as Reservas;");
            Console.WriteLine("5 - Encerrar");
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    PrintTelaNovaReserva();
                    return true;
                case "2":
                    PrintTelaConsultarReservas();
                    return true;
                case "3":
                    //consultar clientes
                    return true;
                case "4":
                    PrintTelaConsultarTodasReservas();
                    return true;
                case "5":
                    // Tela Encerrar
                    return false;
                default:
                    return true;
            }
        }

        public static void PrintTelaConsultarReservas()
        {
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Selecione a Operação Desejada:");
            Console.WriteLine("1 - Realizar Busca Por CPF de Cliente");
            Console.WriteLine("2 - Realizar Busca Por Id da Reserva");
            Console.WriteLine("3 - Retornar");
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    PrintTelaConsultarReserva("CPF");

                    return;
                case "2":
                    PrintTelaConsultarReserva("ID");
                    return;
                case "3":
                    return;
                default:
                    return;
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
            if (cliente.CPF != null)
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
                Console.WriteLine("Pressione Enter Para Continuar");
                Console.ReadLine();
            }
            else
            {
                bool cadastroStatus = false;
                while (!cadastroStatus)
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("CPF Não encontrado Redirecionando Para Novo Cadastro");
                    cadastroStatus = PrintTelaCadastroCliente(cpf);
                }

            }
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Realizar Nova Reserva - Selecionar Quarto:");
            var novaReserva = new CadastrarNova();
            novaReserva.CPF = cpf;
            Console.WriteLine("Selecione Um quarto Tipo de Quarto: ");
            PrintTelaQuartos("Livre", int.Parse(Console.ReadLine()));
            Console.WriteLine("Digite o Número do Quarto Desejado: ");
            novaReserva.QuartoId = int.Parse(Console.ReadLine());
            Console.WriteLine("Data de CheckIn (dd/MM/yyyy): ");
            novaReserva.CheckIn = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Data de CheckOut (dd/MM/yyyy): ");
            novaReserva.CheckOut = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Digite o Numero de Hospedes: ");
            int numHospedes = int.Parse(Console.ReadLine());
            List<Hospede> listaAux = new List<Hospede>();
            for (int i = 0; i < numHospedes; i++)
            {
                var hospede = new Hospede();
                Console.WriteLine($"Digite o Numero do CPF do Hospede {i}: ");
                hospede.CPF = Console.ReadLine();
                listaAux.Add(hospede);
            }
            novaReserva.Hospedes = listaAux;
            Console.WriteLine();
            Console.WriteLine("Pressione Enter Para Continuar");
            ServicoReserva.CadastrarNovaReserva(novaReserva);
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            PrintTelaReserva(novaReserva);
            Console.WriteLine("Pressione Enter Para Continuar");
            Console.ReadLine();
        }

        public static bool PrintTelaCadastroCliente(string cpf)
        {
            var novoCliente = new CadastrarNovo();
            novoCliente.CPF = cpf;
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine($"CPF: {cpf}");
            Console.WriteLine("Nome Completo: ");
            novoCliente.NomeCompleto = Console.ReadLine();
            Console.WriteLine("Data de Nascimento (dd/MM/yyyy): ");
            novoCliente.DataNascimento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Telefone: ");
            novoCliente.Telefone = Console.ReadLine();
            Console.WriteLine("Email: ");
            novoCliente.Email = Console.ReadLine();
            return ServicoCliente.CadastrarNovoCliente(novoCliente);
        }

        public static void PrintTelaQuartos(string status, int tipo)
        {
            //Console.Clear();
            //Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            //Console.WriteLine();
            Console.WriteLine("Quartos Disponivéis:");
            var listaQuartos = ServicoQuarto.BuscarPorStatus(status, tipo);
            listaQuartos.ForEach(q =>
            {
                Console.WriteLine($"{q.QuartoId}, Situação:{ServicoQuarto.ObterSituação(q.SituacaoId).Descricao}");
            });
            Console.WriteLine("Pressione Enter Para Retornar a tela Anterior");
            Console.ReadLine();
        }

        public static void PrintTelaReserva(CadastrarNova reserva)
        {
            Console.WriteLine($"CPF do Cliente: {reserva.CPF}");
            Console.WriteLine($"Quarto: {reserva.QuartoId}");
            Console.WriteLine($"CheckIn: {reserva.CheckIn}");
            Console.WriteLine($"CheckOut: {reserva.CheckOut}");
            Console.WriteLine($"Numero de Hospedes: {reserva.Hospedes.Count}");
        }

        public static void PrintTelaConsultarTodasReservas()
        {
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Reservas: ");
            ServicoReserva.BuscarReservas().ForEach(r =>
            {
                Console.WriteLine("--------------");
                Console.WriteLine($"Reserva ID: {r.ReservaId}");
                Console.WriteLine($"CPF Cliente: {r.CPF}");
                Console.WriteLine($"Data de CheckIn: {r.CheckIn}");
                Console.WriteLine($"Data de CheckOut: {r.CheckOut}");
                Console.WriteLine();
            });
            Console.WriteLine("Pressione Enter Para Retornar");
            Console.ReadLine();
        }

        public static void PrintTelaConsultarReserva(string formatoBusca)
        {
            string identificadorPesquisa;
            string reservaId="";
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("Reservas: ");
            if (formatoBusca == "CPF")
            {
                Console.Write("Digite um CPF: ");
                identificadorPesquisa = Console.ReadLine();
                ServicoReserva.BuscarReservas().ForEach(r =>
                {
                    if (r.CPF == identificadorPesquisa)
                    {
                        reservaId = r.ReservaId;
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine($"Reserva ID: {r.ReservaId}");
                        Console.WriteLine($"CPF Cliente: {r.CPF}");
                        Console.WriteLine($"Data de CheckIn: {r.CheckIn}");
                        Console.WriteLine($"Data de CheckOut: {r.CheckOut}");
                        Console.WriteLine();
                    }
                });
            }
            else
            {
                Console.Write("Digite o ID da reserva: ");
                identificadorPesquisa = Console.ReadLine();
                var reserva = ServicoReserva.BuscarReserva(identificadorPesquisa);
                reservaId = reserva.ReservaId;
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"CPF Cliente: {reserva.CPF}");
                Console.WriteLine($"Data de CheckIn: {reserva.CheckIn}");
                Console.WriteLine($"Data de CheckOut: {reserva.CheckOut}");
                Console.WriteLine($"Quarto: {reserva.QuartoId}");
                Console.WriteLine($"Valores Diarias: R${reserva.ValorDiarias}");
                Console.WriteLine($"Valores Taxas: R${reserva.TaxasConsumo}");
                Console.WriteLine($"Valor Final: R${reserva.ValorFinal}");
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------------------------");
            if (reservaId != "")
            {
                PrintTelaOperacoesReservas(reservaId);
            }
            Console.WriteLine("Pressione Enter Para Retornar");
            Console.ReadLine();
        }

        public static void PrintTelaOperacoesReservas(string reservaId)
        {
            Console.WriteLine("Selecione a Operação Desejada:");
            Console.WriteLine("1 - Realizar CheckIn");
            Console.WriteLine("2 - Realizar CheckOut");
            Console.WriteLine("3 - Adicionar Taxa");
            Console.WriteLine("4 - Retornar");
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    ServicoReserva.RealizarCheckIn(reservaId);
                    return;
                case "2":
                    ServicoReserva.RealizarCheckOut(reservaId);
                    return;
                case "3":
                    Console.Write("Digite Valor da taxa: R$");
                    ServicoReserva.InserirTaxa(double.Parse(Console.ReadLine()),reservaId);
                    return;
                case "4":
                    return;
                default:
                    return;
            }
        }
    }
}
