using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Servicos.ClienteService.Models
{
    class ClienteExceptions : ApplicationException
    {
        public ClienteExceptions(string message): base(message)
        {

        }
    }
}
