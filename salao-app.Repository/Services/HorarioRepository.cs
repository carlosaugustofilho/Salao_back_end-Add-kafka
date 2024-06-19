
using Microsoft.Extensions.Configuration;
using salao_app.Repository.Conexao;
using salao_app.Repository.Interfaces;
using SalaoApp.Models;

namespace salao_app.Repository.Services
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly IConfiguration _configuration;

        public HorarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HorariolMap BuscarHorarioPorId(int horarioId)
        {
            var horario = DataBase.Execute<HorariolMap>(_configuration, "SELECT * FROM horario WHERE horario_id = @Id", new { Id = horarioId }).FirstOrDefault();
            return horario;
        }
        public List<HorariolMap> BuscarTodosHorarios(DateTime data)
        {
            throw new NotImplementedException();
        }

       


        public void AtualizarHorario(HorariolMap horario)
        {
            throw new NotImplementedException();
        }

        public void DeleteHorario(int horarioId)
        {
            throw new NotImplementedException();
        }

       
    }
}
