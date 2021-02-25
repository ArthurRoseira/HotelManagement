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

        public static void CadastrarNovoCliente(CadastrarNovo cliente)
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

        public static List<ConsultarTodos> BuscarClientes()
        {
            // Buscar todos os Clientes e apresentar 
            // Para o usuario
            var clientes = new List<ConsultarTodos>();
            Data.ListaClientes.ForEach(c =>
            {
                var cliente = new ConsultarTodos()
                {
                    NomeCompleto = c.NomeCompleto,
                    Telefone = c.Telefone
                };
                clientes.Add(cliente);
            });
            return clientes;
        }


        public static ConsultaCPF ObterPorCPF(string cpf)
        {
            var clienteObtido = new ConsultaCPF();
            var cliente = Dados.Data.ListaClientes.Find(c => c.CPF == cpf);
            if(cliente != null)
            {
                clienteObtido.CPF = cliente.CPF;
                clienteObtido.NomeCompleto = cliente.NomeCompleto;
                clienteObtido.DataNascimento = cliente.DataNascimento;
                clienteObtido.Email = cliente.Email;
                clienteObtido.Telefone = cliente.Telefone;
            }
            return clienteObtido;
        }


    }
}
