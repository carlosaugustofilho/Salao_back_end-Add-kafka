using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Models.Dto
{
    public class HorarioDisponivelDto
    {
        public int Id { get; set; }
        public int BarbeiroId { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public DateTime Data { get; set; }
    }

}
