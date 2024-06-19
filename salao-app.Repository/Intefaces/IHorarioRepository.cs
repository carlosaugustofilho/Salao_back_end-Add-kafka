using SalaoApp.Models;

namespace salao_app.Repository.Interfaces
{
    public interface IHorarioRepository
    {
        List<HorariolMap> BuscarTodosHorarios(DateTime data);
        HorariolMap BuscarHorarioPorId(int horarioId);
        void AtualizarHorario(HorariolMap horario);
        void DeleteHorario(int horarioId);
    }
}
