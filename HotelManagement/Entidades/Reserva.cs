
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HotelManagement.Servicos.ReservaService.Models;
using System.Text.Json;
using System.Text.Json.Serialization;   

namespace HotelManagement.Entidades
{
    class Reserva
    {
        public string ReservaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CPF { get; set; }
        public string HospedeJSON { get; set; }
        //[NotMapped]
        //public List<ModeloHospede> Hospedes { get; set; }
        public int QuartoId { get; set; }
        public double ValorDiarias { get; set; }
        public double TaxasConsumo { get; set; }
        public double ValorFinal { get; set; }
    
        public string  CriarListaHospedes(List<Hospede> hospedes)
        {
           return JsonSerializer.Serialize(hospedes);
        }
    
    }
}
