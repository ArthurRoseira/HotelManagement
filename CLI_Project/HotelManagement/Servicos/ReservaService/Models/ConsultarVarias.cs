using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Servicos.ReservaService.Models
{
    class ConsultarVarias
    {
        public string ReservaId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CPF { get; set; }
    }
}
