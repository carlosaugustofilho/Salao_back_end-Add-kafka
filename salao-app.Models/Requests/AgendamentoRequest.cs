using System;

namespace salao_app.Models.Requests
{
    public class AgendamentoRequest
    {
        public int ClienteId { get; set; }
        public int BarbeiroId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public int UsuarioId { get; set; }
    }
}
