using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HotelManagement.Servicos.ClienteService;
using HotelManagement.Servicos.ReservaService;
using HotelManagement.Servicos.ReservaService.Models;
using HotelManagement.Servicos.QuartoService;
using HotelManagement.Servicos.ClienteService.Models;
using System.Reflection;

namespace HotelManagement
{
    class Views
    {
        public static bool PrintTelaInicial()
        {
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine("MENU PRINCIPAL:");
            Console.WriteLine();
            Console.WriteLine("1 - Iniciar Nova Reserva;");
            Console.WriteLine("2 - Administrar Reservas");
            Console.WriteLine("3 - Consultar Clientes;");
            Console.WriteLine("4 - Consultar todas as Reservas;");
            Console.WriteLine("5 - Encerrar");
            Console.WriteLine();
            Console.Write("Selecione a Operação Desejada: ");
            switch (Console.ReadLine())
            {
                case "1":
                    var operacaoStatus = PrintTelaNovaReserva();
                    if(operacaoStatus == true)
                    {
                        Console.WriteLine("Operação Bem Sucedida, Presione Enter para Continuar.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Operação Cancelada, Presione Enter para Continuar.");
                        Console.ReadLine();
                    }
                    return true;
                case "2":
                    PrintTelaConsultarReservas();
                    return true;
                case "3":
                    PrintTelaConsultarCliente();
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

        public static void PrintTelaConsultarCliente()
        {
            var statusPesquisa = false;
            while (statusPesquisa == false)
            {
                Console.Clear();
                Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                Console.WriteLine();
                Console.Write("Digite Um CPF: ");

                var cpf = Console.ReadLine();
                if (Dados.Data.ListaClientes.Find(c => c.CPF == cpf) != null)
                {
                    PrintTelaUnicoCliente(cpf);
                    statusPesquisa = true;
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Pressione Enter para Continuar");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("Numero de CPR Inválido");
                    Console.WriteLine("Pressione Enter para Continuar");
                    Console.ReadLine();
                }
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



        public static bool PrintTelaNovaReserva(string message = "")
        {
            try
            {
                if (message != "")
                {
                    Console.WriteLine(message);
                }

                var cliente = new ConsultaCPF();

                Console.Clear();
                Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                Console.WriteLine();
                Console.WriteLine("Realizar Nova Reserva - Selecionar Cliente:");
                Console.WriteLine("Insira um CPF Válido:");
                string cpf = "";
                var cpfCheck = false;
                while (!cpfCheck)
                {
                    cpf = Console.ReadLine();
                    cpfCheck = ServicoReserva.VerificarCpf(cpf);
                    if (cpfCheck == false)
                    {
                        Console.WriteLine();
                        Console.WriteLine("CPF Inválido:");
                        Console.WriteLine();
                        Console.WriteLine("Deseja Cancelar Operação? (S/N)");
                        if (char.Parse(Console.ReadLine()) == 's')
                        {
                            return false;
                        }
                    }
                    cliente = ServicoCliente.ObterPorCPF(cpf);
                    if (cliente.CPF != null)
                    {
                        Console.Clear();
                        Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                        Console.WriteLine();
                        Console.WriteLine("Cliente Ja Cadastrado:");
                        PrintTelaUnicoCliente(cliente.CPF);
                        Console.WriteLine();
                        Console.WriteLine("Pressione Enter Para Continuar");
                        Console.ReadLine();
                    }
                    else
                    {
                        bool cadastroStatus = false;
                        while (!cadastroStatus)
                        {
                            Console.WriteLine("Redirecionando Para Novo Cadastro");
                            System.Threading.Thread.Sleep(2000);
                            cadastroStatus = PrintTelaCadastroCliente(cpf);
                        }

                    }
                    Console.Clear();
                    Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                    Console.WriteLine();
                    Console.WriteLine("Realizar Nova Reserva - Selecionar Quarto:");
                    var novaReserva = new CadastrarNova();
                    novaReserva.CPF = cpf;
                    Console.WriteLine("Selecione Um quarto Tipo de Quarto: (1 - Casal/2 - Simples/3 - Duplo)");
                    PrintTelaQuartos("Livre", int.Parse(Console.ReadLine()));
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("Digite o Número do Quarto Desejado: ");
                    novaReserva.QuartoId = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
                    Console.WriteLine();
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
                    return true;
                }
            }
            catch (ReservaExceptions e)
            {
                Console.WriteLine();
                Console.WriteLine("Deseja Cancelar Operação? (S/N)");
                if (char.Parse(Console.ReadLine()) == 's')
                {
                    return false;
                }
                else
                {
                    PrintTelaNovaReserva(e.Message);
                }
            }
            return true;
        }

        public static bool PrintTelaCadastroCliente(string cpf)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Dados Incorretos Inseridos Pressione Enter para repetir a operação:");
                Console.ReadLine();
                return false;
            }
                
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
            string reservaId = "";
            int iterador = 0;
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
                        iterador += 1;
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
                ServicoReserva.BuscarReservas().ForEach(r =>
                {
                    if (r.ReservaId == identificadorPesquisa)
                    {
                        reservaId = r.ReservaId;
                        iterador += 1;
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine($"Reserva ID: {r.ReservaId}");
                        Console.WriteLine($"CPF Cliente: {r.CPF}");
                        Console.WriteLine($"Data de CheckIn: {r.CheckIn}");
                        Console.WriteLine($"Data de CheckOut: {r.CheckOut}");
                        Console.WriteLine();
                    }
                });
            }
            if (iterador > 1)
            {
                Console.WriteLine();
                Console.Write("Digite o ID da reserva a Ser Alterada: ");
                reservaId = Console.ReadLine();
            }else if(iterador == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Nenhuma Reserva Encontrada para este Cliente");
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
            Console.Clear();
            Console.WriteLine("----------- HOTEL BONSOIR - MANAGEMENT -----------");
            Console.WriteLine();
            Console.WriteLine($"Reserva: {reservaId}");
            PrintTelaUnicaReserva(reservaId);
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Selecione a Operação Desejada:");
            Console.WriteLine("1 - Realizar CheckIn");
            Console.WriteLine("2 - Realizar CheckOut");
            Console.WriteLine("3 - Adicionar Taxa");
            Console.WriteLine("4 - Retornar");
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    var statusCheckIn = false;
                    while (!statusCheckIn)
                    {
                        statusCheckIn = ServicoReserva.RealizarCheckIn(reservaId);
                        PrintTelaUnicaReserva(reservaId);
                        if (!statusCheckIn)
                        {
                            Console.WriteLine("Não foi possivel realizar operação, Reserva Não Encontrada");
                            Console.WriteLine("Pressione Enter Para Cancelar a Operação");
                            Console.ReadLine();
                            statusCheckIn = true;
                        }
                    }
                    return;
                case "2":
                    var statusCheckOut = false;
                    while (!statusCheckOut)
                    {
                        statusCheckOut = ServicoReserva.RealizarCheckOut(reservaId);
                        if (!statusCheckOut)
                        {
                            Console.WriteLine("Não foi possivel realizar operação, Reserva Não Encontrada ou sem ChekIn");
                            Console.WriteLine("Pressione Enter Para Cancelar a Operação");
                            Console.ReadLine();
                            statusCheckOut = true;
                        }
                        Console.WriteLine("Valores relacionados à taxas Extras de Diaria foram automaticamente Adicionados ao valor final.");
                        PrintTelaUnicaReserva(reservaId);
                    }
                    return;
                case "3":
                    Console.Write("Digite Valor da taxa: R$");
                    ServicoReserva.InserirTaxa(double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture), reservaId);
                    PrintTelaUnicaReserva(reservaId);
                    return;
                case "4":
                    return;
                default:
                    return;
            }
        }

        public static void PrintTelaUnicaReserva(string reservaId)
        {
            var reserva = ServicoReserva.BuscarReserva(reservaId);
            Console.WriteLine($"Cliente CPF: {reserva.CPF}");
            Console.WriteLine($"Quarto: {reserva.QuartoId}");
            Console.WriteLine($"CheckIn: {reserva.CheckIn} - {reserva.CheckInStatus.ToUpper()}");
            Console.WriteLine($"CheckOut: {reserva.CheckOut} - {reserva.CheckOutStatus.ToUpper()}");
            reserva.Hospedes.ForEach(hospede =>{
                Console.WriteLine($"Hospede: {hospede.CPF}");
            });
            Console.WriteLine($"Valor Total: {reserva.ValorDiarias}");
            Console.WriteLine($"Valor Total: {reserva.TaxasConsumo}");
            Console.WriteLine($"Valor Total: {reserva.ValorFinal}");
        }


        public static void PrintTelaUnicoCliente(string cpf)
        {
            var cliente = ServicoCliente.ObterPorCPF(cpf);

            Console.WriteLine($"Nome Completo: {cliente.NomeCompleto}");
            Console.WriteLine($"Idade: {ServicoCliente.Idade(cliente.DataNascimento)}");
            Console.WriteLine($"Telefone: {cliente.Telefone}");
            Console.WriteLine($"Email: {cliente.Email}");

        }
    }
}
