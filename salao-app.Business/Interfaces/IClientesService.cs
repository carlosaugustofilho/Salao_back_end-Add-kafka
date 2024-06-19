using salao_app.Models.Dto;
using salao_app.Models.DTOs;
using salao_app.Models.Requests;
using salao_app.Repository.Maps;
using SalaoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Business.Interfaces
{
    public interface IClientesService
    {
        List<ClienteDto> BuscarClientes(string nome, string email);
        ClienteDto BuscarClientesId(int id);
        List<HorarioDisponivelDto> ObterHorariosDisponiveis(int barbeiroId);
        void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId);
        void AtualizarStatusHorario(int horarioId, bool disponivel);
        void CancelarAgendamento(int agendamentoId);

        void CriarCliente(ClienteRequest request);
        void AtualizaDadosCliente(ClienteRequest request);
        void AlterarStatusCliente(int id, bool status);
    }
}
