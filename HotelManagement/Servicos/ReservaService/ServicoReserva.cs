using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ReservaService.Models;
using HotelManagement.Entidades;
using Nanoid;
using System.Text.Json;
using System.Linq;

namespace HotelManagement.Servicos.ReservaService
{
    class ServicoReserva
    {

        public static void CadastrarNovaReserva(CadastrarNova reserva)
        {
            reserva.Validar();
            var tipoQuarto = Dados.Data.ListaQuartos.Find(quarto => quarto.QuartoId == reserva.QuartoId).TipoId;
            var novaReserva = new Reserva()
            {
                CPF = reserva.CPF,
                CheckIn = reserva.CheckIn,
                CheckOut = reserva.CheckOut,
                DataCriacao = DateTime.Now,
                QuartoId = reserva.QuartoId,
                ReservaId = Nanoid.Nanoid.Generate(),
                ValorDiarias = ValorDiarias(tipoQuarto, reserva.CheckIn, reserva.CheckOut),
                TaxasConsumo = 0,
                ValorFinal = 0
            };

            novaReserva.HospedesJSON = JsonSerializer.Serialize(reserva.Hospedes);
            novaReserva.AtualizarValorFinal();
            Dados.Data.ListaReservas.Add(novaReserva);
        }

        public static double ValorDiarias(int tipoQuarto ,DateTime checkIn, DateTime checkOut)
        {

            return (checkIn-checkOut).TotalDays * Dados.Data.ListaTipoQuarto.Find(tipo => tipo.TipoId == tipoQuarto).Valor;
        }


        public static void InserirTaxa(double taxa, string reservaId)
        {
            Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                            .Select(reserva => reserva.TaxasConsumo += taxa).ToList();
        }

        public static void RealizarCheckIn(string reservaId)
        {
            Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                .Select(reserva => reserva.CheckInStatus = "ok").ToList();

        }

        public static void RealizarCheckOut(string reservaId)
        {
            Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                .Select(reserva => reserva.CheckOutStatus = "ok").ToList();

        }


        public static List<ConsultarVarias> BuscarReservas()
        {
            // Buscar todos As Reservas e apresentar 
            // Para o usuario
            var reservas = new List<ConsultarVarias>();
            Dados.Data.ListaReservas.ForEach(r =>
            {
                var reserva = new ConsultarVarias()
                {
                    ReservaId = r.ReservaId,
                    CPF = r.CPF,
                    CheckIn = r.CheckIn,
                    CheckOut = r.CheckOut,

                };
                reservas.Add(reserva);
            });
            return reservas;
        }

        public static ConsultarUma BuscarReserva(string reservaID)
        {
            // Buscar todos As Reservas e apresentar 
            // Para o usuario
            var reserva = new ConsultarUma();
            Dados.Data.ListaReservas.ForEach(r =>
            {
                reserva.CPF = r.CPF;
                reserva.CheckIn = r.CheckIn;
                reserva.CheckOut = r.CheckOut;
                reserva.QuartoId = r.QuartoId;
                //Hospedes = 
                reserva.ValorDiarias = r.ValorDiarias;
                reserva.TaxasConsumo = r.TaxasConsumo;
                reserva.ValorFinal = r.ValorFinal;
            });
            return reserva;
        }


    }
}
