using salao_app.Business.Interfaces;
using salao_app.Models.Dto;
using salao_app.Models.DTOs;
using salao_app.Models.Requests;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace salao_app.Business.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClienteRepository _repository;

        public ClientesService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public void CriarCliente(ClienteRequest request)
        {
            var map = new ClienteMap
            {
                nome = request.nome,
                usuarioId = request.usuarioId,
                email = request.email,
            };

            if (ExisteCliente(map))
            {
                throw new Exception("Cliente já cadastrado!");
            }

            _repository.CriarCliente(map);
        }

        public List<ClienteDto> BuscarClientes(string nome, string email)
        {
            var resposta = _repository.BuscarClientes(nome, email);
            return new ClienteDto().ToDtoList(resposta.OrderBy(c => c.nome).ToList());
        }

        public ClienteDto BuscarClientesId(int id)
        {
            var resposta = _repository.BuscarClientePorId(id);
            if (resposta == null)
            {
                return null;
            }

            return new ClienteDto
            {
                ClienteId = resposta.clienteId,
                Nome = resposta.nome,
                Email = resposta.email,
            };
        }

        public void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId)
        {
            _repository.AgendarHorarioCliente(clienteId, barbeiroId, data, horaInicio, horaFim, usuarioId);
        }

        public List<HorarioDisponivelDto> ObterHorariosDisponiveis(int barbeiroId)
        {
            var horariosMap = _repository.ObterHorariosDisponiveis(barbeiroId);
            var horariosDto = horariosMap.Select(h => new HorarioDisponivelDto
            {
                Id = h.Id,
                BarbeiroId = h.BarbeiroId,
                HoraInicio = h.HoraInicio,
                HoraFim = h.HoraFim,
                Data = h.Data
            }).ToList();

            return horariosDto;
        }
        public void AtualizarStatusHorario(int horarioId, bool disponivel)
        {
            _repository.AtualizarStatusHorario(horarioId, disponivel);
        }


        public void CancelarAgendamento(int agendamentoId)
        {
            _repository.CancelarAgendamento(agendamentoId);
        }

        public void AlterarStatusCliente(int id, bool status)
        {
            throw new NotImplementedException();
        }

        public void AtualizaDadosCliente(ClienteRequest request)
        {
            throw new NotImplementedException();
        }

        private bool ExisteCliente(ClienteMap cliente)
        {
            return _repository.ExistCliente(cliente);
        }

       
    }
}
