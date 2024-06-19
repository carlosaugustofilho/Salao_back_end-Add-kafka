using salao_app.Repository.Maps;
using System;

namespace salao_app.Repository.Services
{
    public interface IBarbeiroRepository
    {
        void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim);
        List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId);
        List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data);
        BarbeiroMap BuscarBarbeiroPorId(int usuarioId); // Mudança no tipo de retorno
        public List<AgendamentoMap> ListarTodosAgendamentos();
        void CriarBarbeiro(BarbeiroMap barbeiro);
    }
}
