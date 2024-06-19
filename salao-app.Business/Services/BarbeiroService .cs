using carvao_app.Repository.Helper;
using salao_app.Models.Dtos;
using salao_app.Models.Requests;
using salao_app.Repository.Enum;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Maps;
using salao_app.Repository.Services;
using salao_app.Services.Interfaces;
using System;

namespace salao_app.Services
{
    public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _barbeiroRepository;
        private IUsuarioRepository _usuarioRepository;

        public BarbeiroService(IBarbeiroRepository barbeiroRepository, IUsuarioRepository usuarioRepository)
        {
            _barbeiroRepository = barbeiroRepository;
            _usuarioRepository = usuarioRepository;
        }

        public void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var duracaoAtendimento = TimeSpan.FromMinutes(45);
            var horaAtual = horaInicio;

            while (horaAtual + duracaoAtendimento <= horaFim)
            {
                _barbeiroRepository.AdicionarHorarioDisponivel(barbeiroId, data, horaAtual, horaAtual + duracaoAtendimento);
                horaAtual += duracaoAtendimento;
            }
        }

        public List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId)
        {
            return _barbeiroRepository.ListarHorariosDisponiveis(barbeiroId);
        }

      
        public void CriarBarbeiro(BarbeiroRequest request)
        {
            var usuario = new UsuarioMap
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = Cripto.Encrypt(request.Senha),
                DataRegistro = DateTime.Now,
                PapelId = (int)TipoUsuario.Barbeiro
            };

            _usuarioRepository.CadastroUsuario(usuario);

            var barbeiro = new BarbeiroMap
            {
                Nome = request.Nome,
                Email = request.Email,
                Descricao = request.Descricao,
                UrlFoto = request.UrlFoto,
                UsuarioId = usuario.Id
            };

            _barbeiroRepository.CriarBarbeiro(barbeiro);
        }

        public List<AgendamentoMap> ListarTodosAgendamentos()
        {
            return _barbeiroRepository.ListarTodosAgendamentos();
        }

        public BarbeiroDto BuscarBarbeiroPorId(int barbeiroId)
        {
            throw new NotImplementedException();
        }
    }


}

