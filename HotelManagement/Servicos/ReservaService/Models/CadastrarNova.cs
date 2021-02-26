using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Servicos.ReservaService.Models
{
    class CadastrarNova
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CPF { get; set; }
        public List<Hospede> Hospedes { get; set; }
        public int QuartoId { get; set; }


        public void Validar()
        {
            if (CPF.Length < 11)
                throw new ReservaExceptions("CPF Invalido");
            if (CheckIn < DateTime.Now && CheckIn> CheckOut)
                throw new ReservaExceptions("Data De CheckIn Inválida");
            if (CheckOut < DateTime.Now && CheckOut > CheckIn)
                throw new ReservaExceptions("Data De CheckOut Inválida");
            Hospedes.ForEach(hospede=>
            {
                if (hospede.CPF.Length < 11)
                    
                    throw new ReservaExceptions("CPF Invalido");
            });
        }
    }
}
