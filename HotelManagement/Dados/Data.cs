using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HotelManagement.Entidades;
using System.Globalization;


namespace HotelManagement.Dados
{
    class Data
    {
        public static List<Cliente> ListaClientes = new List<Cliente>();
        public static List<Reserva> ListaReservas = new List<Reserva>();
        public static List<Quarto> ListaQuartos = new List<Quarto>();
        public static List<SituacaoQuarto> ListaSituacaoQuartos = new List<SituacaoQuarto>();
        public static List<TipoQuarto> ListaTipoQuarto = new List<TipoQuarto>();
        //public static string DadosLocal = "C:\\Users\\arthur.santos\\Documents\\Curso C# Basico\\HotelManagement\\HotelManagement\\Dados";
        public static string DadosLocal = Environment.CurrentDirectory.Replace("bin\\Debug\\netcoreapp3.1", "Dados");
        public static void CarregarDados()
        {
            CarregarDadosCliente();
            CarregarDadosQuartos();
            CarregarDadosReservas();
            CarregarDadosSituacaoQuartos();
            CarregarDadosTipoQuarto();
        }

        public static void SalvarDados()
        {
            SalvarDadosClientes();
            SalvarDadosReservas();
        }


        public static void CarregarDadosCliente()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("Clientes"))
                {
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            if (linhas != null)
                            {
                                var values = linhas.Split(',');
                                var cliente = new Cliente()
                                {
                                    CPF = values[0],
                                    NomeCompleto = values[1],
                                    Telefone = values[2],
                                    DataNascimento = DateTime.Parse(values[3]),
                                    Email = values[4],
                                    DataCriacao = DateTime.Parse(values[5])
                                };
                                ListaClientes.Add(cliente);
                            }
                        }
                    }
                }

            }
        }

        public static void CarregarDadosQuartos()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("Quartos"))
                {
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            if (linhas != null)
                            {
                                var values = linhas.Split(',');
                                var quarto = new Quarto()
                                {
                                    QuartoId = int.Parse(values[0]),
                                    SituacaoId = int.Parse(values[2]),
                                    TipoId = int.Parse(values[1])
                                };
                                ListaQuartos.Add(quarto);
                            }
                        }
                    }
                }

            }

        }
        public static void CarregarDadosReservas()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("Reserva"))
                {
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            if (linhas != null)
                            {
                                var values = linhas.Split(';');
                                var reserva = new Reserva()
                                {
                                    ReservaId = values[0],
                                    DataCriacao = DateTime.Parse(values[1]),
                                    CheckIn = DateTime.Parse(values[2]),
                                    CheckInStatus = values[3],
                                    CheckOut = DateTime.Parse(values[4]),
                                    CheckOutStatus = values[5],
                                    CPF = values[6],
                                    HospedesJSON = values[7],
                                    QuartoId = int.Parse(values[8]),
                                    ValorDiarias = double.Parse(values[9], CultureInfo.InvariantCulture),
                                    TaxasConsumo = double.Parse(values[10], CultureInfo.InvariantCulture),
                                    ValorFinal = double.Parse(values[11], CultureInfo.InvariantCulture)
                                };
                                ListaReservas.Add(reserva);
                            }
                        }
                    }
                }

            }
        }

        public static void CarregarDadosSituacaoQuartos()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("SituacaoQuarto"))
                {
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            if (linhas != null)
                            {
                                var values = linhas.Split(',');
                                var situacaoQuarto = new SituacaoQuarto
                                {
                                    SituacaoId = int.Parse(values[0]),
                                    Descricao = values[1],
                                };
                                ListaSituacaoQuartos.Add(situacaoQuarto);
                            }
                        }
                    }
                }

            }

        }

        public static void CarregarDadosTipoQuarto()
        {
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("TipoQuarto"))
                {
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream )
                        {
                            var linhas = reader.ReadLine();
                            if (linhas != null)
                            {
                                var values = linhas.Split(',');
                                var tipoQuarto = new TipoQuarto()
                                {
                                    TipoId = int.Parse(values[0]),
                                    Descricao = values[1],
                                    Valor = double.Parse(values[2], CultureInfo.InvariantCulture)
                                };
                                ListaTipoQuarto.Add(tipoQuarto);
                            }
                        }
                    }
                }

            }

        }


        public static void SalvarDadosClientes()
        {
            var cpfExistentes = new List<string>();
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("Clientes"))
                {
                    List<string> lista = new List<string>();
                    lista.Add("CPF,NomeCompleto,Telefone,DataNascimento,Email,DataCriacao");
                    ListaClientes.ForEach(c => lista.Add(c.ToString()));
                    File.WriteAllLines(arquivos[i], lista);
                }
            }
        }

        public static void SalvarDadosReservas()
        {
            var reservasExistentes = new List<string>();
            string[] arquivos = Directory.GetFiles(DadosLocal, "*.csv", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < arquivos.Length; i++)
            {
                if (arquivos[i].Contains("Reserva"))
                {
                    List<string> lista = new List<string>();
                    lista.Add("ReservaId;DataCriacao;CheckIn;CheckInStatus;CheckOut;CheckOutStatus;CPF;HospedesJSON;QuartoId;ValoresDiarias;TaxasConsumo;ValorFinal");
                    ListaReservas.ForEach(r => lista.Add(r.ToString()));
                    File.WriteAllLines(arquivos[i],lista);
                }
            }
        }



    }
}
