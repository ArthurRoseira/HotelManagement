
using System;
using System.Collections.Generic;
using System.Text;

using HotelManagement.Servicos.ReservaService.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Entidades
{
    class Reserva
    {
        public string ReservaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime CheckIn { get; set; }
        public string CheckInStatus { get; set; } = "nok";
        public DateTime CheckOut { get; set; }
        public string CheckOutStatus { get; set; } = "nok";
        public string CPF { get; set; }
        public string HospedesJSON { get; set; }
        //public string HospedesJSON { 
        //    get 
        //    {
        //        if (this.Hospedes.Count == 0)
        //            return null;
        //        return JsonSerializer.Serialize(Hospedes);
        //    }
        //    set 
        //    {
        //        this.Hospedes.Clear();

        //        if(value != null){
        //            this.Hospedes = JsonSerializer.Deserialize<List<Hospede>>(value);
        //        }
        //    }
        //}
        public List<Hospede> Hospedes { get; set; }
        public int QuartoId { get; set; }
        public double ValorDiarias { get; set; }
        public double TaxasConsumo { get; set; }
        public double ValorFinal { get; set; }


        public string CriarListaHospedes(List<Hospede> hospedes)
        {
            return JsonSerializer.Serialize(hospedes);
        }

        public List<Hospede> DeserializarHospedes(string hospedes)
        {
            return JsonSerializer.Deserialize<List<Hospede>>(hospedes);
        }


        public void AtualizarValorFinal()
        {
            ValorFinal = ValorDiarias + TaxasConsumo;
        }

        public override string ToString()
        {
            return string.Join(";", string.Join(";", ReservaId, DataCriacao.ToString(), CheckIn, CheckInStatus ,CheckOut, CheckOutStatus,CPF,HospedesJSON, QuartoId, ValorDiarias.ToString("0.00"), TaxasConsumo.ToString("0.00"), ValorFinal.ToString("0.00")));
        }


    }
}
