using salao_app.Models.Dtos;
using salao_app.Models.Requests;
using salao_app.Repository.Maps;

namespace salao_app.Services.Interfaces
{
    public interface IBarbeiroService
    {
        void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim);

        List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId);


        BarbeiroDto BuscarBarbeiroPorId(int barbeiroId);

        void CriarBarbeiro(BarbeiroRequest reques);

        List<AgendamentoMap> ListarTodosAgendamentos();



    }
}
