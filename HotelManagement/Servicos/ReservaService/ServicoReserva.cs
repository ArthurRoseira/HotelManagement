using System;
using System.Collections.Generic;
using System.Text;
using HotelManagement.Servicos.ReservaService.Models;
using HotelManagement.Entidades;
using Nanoid;
namespace HotelManagement.Servicos.ReservaService
{
    class ServicoReserva
    {

        public static void CadastrarNovaReserva(CadastrarNova reserva)
        {
            reserva.Validar();
            var novaReserva = new Reserva()
            {
                CPF = reserva.CPF,
                CheckIn = reserva.CheckIn,
                CheckOut = reserva.CheckOut,
                DataCriacao = DateTime.Now,
                QuartoId = reserva.QuartoId,
                ReservaId = Nanoid.Nanoid.Generate(),
                ValorDiarias = 0,
                TaxasConsumo = 0,
                ValorFinal = 0,
            };
            novaReserva.CriarListaHospedes(reserva.Hospedes);
            Dados.Data.ListaReservas.Add(novaReserva);
        }
    }
}
