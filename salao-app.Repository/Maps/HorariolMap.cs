using salao_app.Repository.Maps;
using System;

namespace SalaoApp.Models
{
    public class HorariolMap
    {
        public int HorarioId { get; set; }
    
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
         public bool Disponivel { get; set; }

        public BarbeiroMap Barbeiro { get; set; }
        public ClienteMap Cliente { get; set; }
    }
}
