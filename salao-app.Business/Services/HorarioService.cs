using System;
using System.Collections.Generic;
using salao_app.Business.Interfaces;
using salao_app.Models;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Maps;
using salao_app.Repository.Services;
using SalaoApp.Models;

namespace salao_app.Business.Services
{
    public class HorarioService : IHorarioService
    {
        private readonly IHorarioRepository _horarioDisponivelRepository;

        public HorarioService(IHorarioRepository horarioDisponivelRepository)
        {
            _horarioDisponivelRepository = horarioDisponivelRepository;
        }

        public List<HorariolMap> BuscarTodosHorarios(DateTime data)
        {
            HorariolMap horario = new HorariolMap();
            return _horarioDisponivelRepository.BuscarTodosHorarios(data);
        }
 
   
        public HorariolMap BuscarHorarioPorId(int horarioId)
        {
            return _horarioDisponivelRepository.BuscarHorarioPorId(horarioId);
        }



        public void AtualizarHorario(HorariolMap horario)
        {
            _horarioDisponivelRepository.AtualizarHorario(horario);
        }

        public void DeletarHorario(int horarioId)
        {
            _horarioDisponivelRepository.DeleteHorario(horarioId);
        }

       
    }
}
