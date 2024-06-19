using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using salao_app.Repository.Conexao;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace salao_app.Repository.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConfiguration _configuration;

        public ClienteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClienteMap BuscarClientePorId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return DataBase.Execute<ClienteMap>(_configuration, "SELECT * FROM cliente WHERE cliente_id = @Id", parameters).FirstOrDefault();
        }

        public void CriarCliente(ClienteMap cliente)
        {
            // Verificar se o cliente já existe com o mesmo email
            var parametersCheck = new DynamicParameters();
            parametersCheck.Add("@Email", cliente.email);

            var queryCheck = @"SELECT COUNT(1) FROM cliente WHERE email = @Email";
            var clientExists = DataBase.Execute<int>(_configuration, queryCheck, parametersCheck).FirstOrDefault() > 0;

            if (clientExists)
            {
                throw new Exception("Já existe um cliente registrado com este email.");
            }

            var clientes = new DynamicParameters();
            clientes.Add("@nome", cliente.nome);
            clientes.Add("@usuario_id", cliente.usuarioId);
            clientes.Add("@email", cliente.email);

            var query = "INSERT INTO cliente(nome, usuario_id, email) VALUES (@nome, @usuario_id, @email)";
            DataBase.Execute(_configuration, query, clientes);
        }


        public ClienteMap BuscarClientePorUsuarioId(int usuarioId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", usuarioId);

            var query = "SELECT * FROM Cliente WHERE usuario_id = @UsuarioId";
            return DataBase.Execute<ClienteMap>(_configuration, query, parameters).FirstOrDefault();
        }


        public List<ClienteMap> BuscarClientes(string nome, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@nome", "%" + nome + "%");
            var query = "SELECT cliente_id clienteId, nome, email FROM cliente WHERE nome LIKE @nome";

            if (!string.IsNullOrEmpty(email))
            {
                parameters.Add("@email", email);
                query += " AND email = @email";
            }

            var retorno = DataBase.Execute<ClienteMap>(_configuration, query, parameters).ToList();

            
            return retorno.OrderBy(c => c.clienteId).ToList();
        }

        public void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId)
        {
            
                      
            if (data == DateTime.MinValue) data = DateTime.Now.Date;
            if (data < DateTime.Now.Date) throw new ArgumentException("Data não pode ser anterior ao dia atual.");
            if (horaInicio == TimeSpan.Zero || horaFim == TimeSpan.Zero) throw new ArgumentException("Horas de início e fim não podem ser nulas.");
            if (barbeiroId <= 0) throw new ArgumentException("Identificador do barbeiro é inválido.");

            var horarioId = VerificarHorarioDisponivel(barbeiroId, data, horaInicio, horaFim);
            if (horarioId == 0) throw new Exception("Não há horário disponível para este dia e hora.");

            var parameters = new DynamicParameters();
            parameters.Add("@cliente_id", clienteId);
            parameters.Add("@barbeiro_id", barbeiroId);
            parameters.Add("@data_hora_agendamento", data.Add(horaInicio));
            parameters.Add("@status", "Agendado");
            parameters.Add("@horario_disponiveis_id", horarioId);

            var query = "INSERT INTO agendamentos (cliente_id, barbeiro_id, data_hora_agendamento, status, horario_disponiveis_id) VALUES (@cliente_id, @barbeiro_id, @data_hora_agendamento, @status, @horario_disponiveis_id)";
            DataBase.Execute(_configuration, query, parameters);

            var updateParameters = new DynamicParameters();
            updateParameters.Add("@horarioId", horarioId);
            var updateQuery = "UPDATE horario_disponivel SET disponivel = 0 WHERE id = @horarioId";
            DataBase.Execute(_configuration, updateQuery, updateParameters);
        }


        private int VerificarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var query = @"
                SELECT id
                FROM horario_disponivel
                WHERE barbeiro_id = @barbeiroId
                AND data = @data
                AND hora_inicio <= @horaInicio
                AND hora_fim >= @horaFim
                AND disponivel IS NOT NULL
            ";

            var parameters = new DynamicParameters();
            parameters.Add("@barbeiroId", barbeiroId);
            parameters.Add("@data", data.Date);
            parameters.Add("@horaInicio", horaInicio);
            parameters.Add("@horaFim", horaFim);

            var horarioId = DataBase.Execute<int>(_configuration, query, parameters).FirstOrDefault();
            return horarioId;
        }


        public void CancelarAgendamento(int agendamentoId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@agendamentoId", agendamentoId);

            var queryObterHorario = @"
                SELECT horario_disponiveis_id
                FROM agendamentos
                WHERE agendamento_id = @agendamentoId AND status = 'Agendado'
            ";
            var horarioId = DataBase.Execute<int>(_configuration, queryObterHorario, parameters).FirstOrDefault();

            if (horarioId == 0)
                throw new Exception("Agendamento não encontrado ou já cancelado.");

            var queryCancelarAgendamento = @"
                UPDATE agendamentos
                SET status = 'Cancelado'
                WHERE agendamento_id = @agendamentoId
            ";
            DataBase.Execute(_configuration, queryCancelarAgendamento, parameters);

            var updateParameters = new DynamicParameters();
            updateParameters.Add("@horarioId", horarioId);

            var updateQuery = "UPDATE horario_disponivel SET disponivel = 1 WHERE id = @horarioId";
            DataBase.Execute(_configuration, updateQuery, updateParameters);
        }

        public List<HorarioDisponivelMap> ObterHorariosDisponiveis(int barbeiroId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@barbeiroId", barbeiroId);

            var query = @"
            SELECT id AS Id, barbeiro_id AS BarbeiroId, hora_inicio AS HoraInicio, hora_fim AS HoraFim, data AS Data 
            FROM horario_disponivel 
            WHERE barbeiro_id = @barbeiroId AND disponivel = 1
            ORDER BY data, hora_inicio";  

            return DataBase.Execute<HorarioDisponivelMap>(_configuration, query, parameters).ToList();
        }


        public void AtualizarStatusHorario(int id, bool disponivel)
        {
            var query = "UPDATE horario_disponivel SET disponivel = @disponivel WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@disponivel", disponivel ? 1 : 0);

            DataBase.Execute(_configuration, query, parameters);
        }

        public void AtualizarCliente(ClienteMap cliente)
        {
            throw new NotImplementedException();
        }

        public void DeletarCliente(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistCliente(ClienteMap request)
        {
            var query = !string.IsNullOrEmpty(request.email) ? "Email = @Email" : "nome = @nome";

            using var connection = new MySqlConnection(_configuration["DefaultConnection"]);

            var exist = connection.QueryFirstOrDefault<ClienteMap>("SELECT * FROM cliente WHERE " + query, new
            {
                request.email,

            });

            return exist != null;
        }
    }
}
