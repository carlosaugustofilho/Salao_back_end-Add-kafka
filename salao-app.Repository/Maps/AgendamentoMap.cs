using salao_app.Repository.Enum;

namespace salao_app.Repository.Maps
{
    public class AgendamentoMap
    {
        public int AgendamentoId { get; set; }
        public int UsuarioId { get; set; }
        public int BarbeiroId { get; set; }
        public DateTime DataHoraAgendamento { get; set; }
        public StatusAgendamento Status { get; set; }
        public string? Observacoes { get; set; }
    }
}
