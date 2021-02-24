
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagement.Entidades
{
    class Reserva
    {
        public double ReservaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double CPF { get; set; }
        public string HospedeJSON { get; set; }
        //[NotMapped]
        //public List<ModeloHospede> Hospedes { get; set; }
        public int QuartoId { get; set; }
        public double ValorDiarias { get; set; }
        public double TaxasConsumo { get; set; }
        public double ValorFinal { get; set; }
    }
}
