﻿using System;
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
                CheckIn = reserva.CheckIn.Add(new TimeSpan(18,0,0)),
                CheckOut = reserva.CheckOut.Add(new TimeSpan(12, 0, 0)),
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

            return Math.Ceiling((checkOut-checkIn).TotalDays) * Dados.Data.ListaTipoQuarto.Find(tipo => tipo.TipoId == tipoQuarto).Valor;
        }

        public static bool VerificarCpf(string cpf)
        {
            if (cpf.Length < 11)
            {
                return false;
            }
            else
            {
                foreach (char c in cpf)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }
            return true;
        }

        public static void InserirTaxa(double taxa, string reservaId)
        {
            Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                            .Select(reserva => reserva.TaxasConsumo += taxa).ToList();
            Dados.Data.ListaReservas.Find(reserva => reserva.ReservaId == reservaId).AtualizarValorFinal();
        }

        public static bool RealizarCheckIn(string reservaId)
        {
            var reserva = Dados.Data.ListaReservas.Find(reserva => reserva.ReservaId == reservaId);
            if (reserva != null)
            {
                Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                .Select(reserva => reserva.CheckInStatus = "ok").ToList();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RealizarCheckOut(string reservaId)
        {
            var reserva = Dados.Data.ListaReservas.Find(reserva => reserva.ReservaId == reservaId);
            if (reserva.CheckInStatus == "ok" && reserva != null)
            {

                Dados.Data.ListaReservas.ForEach(reserva =>
                {
                    if (reserva.ReservaId == reservaId)
                    {
                        var tipoQuarto = Dados.Data.ListaQuartos.Find(quarto => quarto.QuartoId == reserva.QuartoId).TipoId;
                        reserva.CheckOutStatus = "ok";
                        if (reserva.CheckOut < DateTime.Now)
                        {
                            reserva.TaxasConsumo += Math.Round(Dados.Data.ListaTipoQuarto.Find(tipo => tipo.TipoId == tipoQuarto).Valor 
                            * (Math.Ceiling((DateTime.Now - reserva.CheckOut).TotalDays)),2);
                        }
                        reserva.ValorDiarias = ServicoReserva.ValorDiarias(tipoQuarto,reserva.CheckIn,reserva.CheckOut);
                    }
                });

                //Dados.Data.ListaReservas.Where(reserva => reserva.ReservaId == reservaId)
                //.Select(reserva => reserva.CheckOutStatus = "ok").ToList();
                reserva.AtualizarValorFinal();
                return true;

            }
            else
            {
                    return false;
            }

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
                if(r.ReservaId == reservaID)
                {
                    reserva.CPF = r.CPF;
                    reserva.CheckIn = r.CheckIn;
                    reserva.CheckInStatus = r.CheckInStatus;
                    reserva.CheckOut = r.CheckOut;
                    reserva.CheckOutStatus = r.CheckOutStatus;
                    reserva.QuartoId = r.QuartoId;
                    reserva.Hospedes = r.DeserializarHospedes(r.HospedesJSON);
                    reserva.ValorDiarias = r.ValorDiarias;
                    reserva.TaxasConsumo = r.TaxasConsumo;
                    reserva.ValorFinal = r.ValorFinal;
                }
            });
            return reserva;
        }


    }
}
