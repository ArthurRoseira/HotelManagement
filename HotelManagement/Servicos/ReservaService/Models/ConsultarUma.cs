using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Servicos.ReservaService.Models
{
    class ConsultarUma
    {
        public string ReservaId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CPF { get; set; }
        public List<Hospede> Hospedes { get; set; }
        public int QuartoId { get; set; }
        public double ValorDiarias { get; set; }
        public double TaxasConsumo { get; set; }
        public double ValorFinal { get; set; }

    }
}
