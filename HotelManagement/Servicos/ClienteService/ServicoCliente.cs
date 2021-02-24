using System;
using System.Collections.Generic;
using HotelManagement.Dados;
using HotelManagement.Servicos.ClienteService.Models;
using HotelManagement.Entidades;
using System.Linq;

namespace HotelManagement.Servicos.ClienteService
{
    class ServicoCliente
    {
        //Conectar Com Banco
        // Cadastrar Novo Cliente
        // Atualizar dados Cliente
        //public string DadosLocal = "C:\\Users\\arthur.santos\\Documents\\Curso C# Basico\\HotelManagement\\HotelManagement\\Dados";
        //public string TabelaNome = "Clientes";

        public static void CadastrarNovo(ModeloCadastro cliente)
        {
            cliente.Validar();
            var novoCliente = new Cliente() {
                CPF = cliente.CPF,
                NomeCompleto = cliente.NomeCompleto,
                DataNascimento = cliente.DataNascimento,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                DataCriacao = DateTime.Now
            };

            if (Data.ListaClientes.Any(c => c.CPF == cliente.CPF))
                throw new ClienteExceptions("Este Cliente Ja Existe");

            novoCliente.CPF = cliente.CPF;
            novoCliente.NomeCompleto = cliente.NomeCompleto;
            novoCliente.DataNascimento = cliente.DataNascimento;
            novoCliente.Email = cliente.Email;
            novoCliente.Telefone = cliente.Telefone;
            Data.ListaClientes.Add(novoCliente);
        }

        public static List<ModeloLista> Buscar()
        {
            var clientes = new List<ModeloLista>();
            Data.ListaClientes.ForEach(c =>
            {
                var cliente = new ModeloLista()
                {
                    NomeCompleto = c.NomeCompleto,
                    Telefone = c.Telefone
                };
                clientes.Add(cliente);
            });
            return clientes;
        }



    }
}
