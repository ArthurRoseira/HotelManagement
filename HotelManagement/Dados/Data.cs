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
        public static List<Quarto> ListaQuartos = new List<Quarto>();
        public static List<SituacaoQuarto> ListaSituacaoQuartos = new List<SituacaoQuarto>();
        public static List<TipoQuarto> ListaTipoQuarto = new List<TipoQuarto>();
        public static string DadosLocal = "C:\\Users\\arthur.santos\\Documents\\Curso C# Basico\\HotelManagement\\HotelManagement\\Dados";


        public static void CarregarDados()
        {
            CarregarDadosCliente();
            CarregarDadosQuartos();
            CarregarDadosSituacaoQuartos();
            CarregarDadosTipoQuarto();
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
                            var values = linhas.Split(',');
                            var quarto = new Quarto()
                            {
                                QuartoId = int.Parse(values[0]),
                                SituacaoId = int.Parse(values[1]),
                                TipoId = int.Parse(values[2])
                            };
                            ListaQuartos.Add(quarto);
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
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            var values = linhas.Split(',');
                            var tipoQuarto = new TipoQuarto()
                            {
                                TipoId = int.Parse(values[0]),
                                Descricao = values[1],
                                Valor = double.Parse(values[2])
                            };
                            ListaTipoQuarto.Add(tipoQuarto);
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
                    using (var reader = new StreamReader(arquivos[i]))
                    {
                        string Cabecalho = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var linhas = reader.ReadLine();
                            var valores = linhas.Split(',');
                            cpfExistentes.Add(valores[0]);
                        }
                    }


                }
                using (StreamWriter sw = File.AppendText(arquivos[i]))
                {
                    sw.WriteLine();
                    ListaClientes.ForEach(cliente =>
                {
                    var checarCpf = cpfExistentes.Find(cpf => cpf == cliente.CPF);
                    if (checarCpf == null)
                    {
                        sw.WriteLine(cliente.ToString());
                    }
                });
                };
            }
        }

       

    }
}
