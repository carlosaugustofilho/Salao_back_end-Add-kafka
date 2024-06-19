using SalaoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Repository.Maps
{
    public class ClienteMap
    {
        public int clienteId { get; set; }
        public int usuarioId { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public BarbeiroMap Barbeiro { get; set; }
        public HorariolMap Horario { get; set; }
    }
}
