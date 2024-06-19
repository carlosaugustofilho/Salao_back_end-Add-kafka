using Dapper;
using Microsoft.Extensions.Configuration;
using salao_app.Repository.Conexao;
using salao_app.Repository.Maps;


namespace salao_app.Repository.Services
{
    public class BarbeiroRepository : IBarbeiroRepository
    {
        private readonly IConfiguration _configuration;

        public BarbeiroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void AdicionarHorarioDisponivel(int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim)
        {
            var barbeiro = DataBase.Execute<BarbeiroMap>(_configuration, "SELECT * FROM barbeiros WHERE barbeiro_id = @BarbeiroId", new { BarbeiroId = barbeiroId }).FirstOrDefault();

            if (barbeiro == null)
            {
                throw new ArgumentException("Barbeiro não encontrado.");
            }

            // Ajuste de fuso horário ou formatação, se necessário
            var dataUtc = data.ToUniversalTime();

            var query = @"
            INSERT INTO Horario_disponivel (barbeiro_id, data, hora_inicio, hora_fim)
            VALUES (@BarbeiroId, @Data, @HoraInicio, @HoraFim)";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);
            parameters.Add("Data", dataUtc);
            parameters.Add("HoraInicio", horaInicio);
            parameters.Add("HoraFim", horaFim);

            DataBase.Execute(_configuration, query, parameters);
        }


        public List<HorarioDisponivelMap> ListarHorariosDisponiveis(int barbeiroId)
        {
                var query = @"
            SELECT 
                id AS Id, 
                barbeiro_id AS BarbeiroId, 
                data AS Data, 
                hora_inicio AS HoraInicio, 
                hora_fim AS HoraFim
            FROM 
                Horario_disponivel
            WHERE 
                barbeiro_id = @BarbeiroId";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);

            var result = DataBase.Execute<HorarioDisponivelMap>(_configuration, query, parameters).ToList();

            result.ForEach(h => Console.WriteLine($"ID: {h.Id}, BarbeiroId: {h.BarbeiroId}, Data: {h.Data}, HoraInicio: {h.HoraInicio}, HoraFim: {h.HoraFim}"));

            return result;
        }


        public List<HorarioDisponivelMap> ListarHorariosDisponiveisPorData(int barbeiroId, DateTime data)
        {
            var query = @"
                SELECT id, barbeiro_id, data, hora_inicio, hora_fim
                FROM Horario_disponivel
                WHERE barbeiro_id = @BarbeiroId AND data = @Data";

            var parameters = new DynamicParameters();
            parameters.Add("BarbeiroId", barbeiroId);
            parameters.Add("Data", data);

            var result = DataBase.Execute<HorarioDisponivelMap>(_configuration, query, parameters).ToList();
            
            return result;
        }


        public BarbeiroMap BuscarBarbeiroPorId(int barbeiroId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BarbeiroId", barbeiroId);
            return DataBase.Execute<BarbeiroMap>(_configuration, "SELECT * FROM barbeiros WHERE barbeiro_id = @BarbeiroId", parameters).FirstOrDefault();
        }

        public void CriarBarbeiro(BarbeiroMap barbeiro)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nome", barbeiro.Nome);
            parameters.Add("@Email", barbeiro.Email);
            parameters.Add("@Descricao", barbeiro.Descricao);
            parameters.Add("@UrlFoto", barbeiro.UrlFoto);
            parameters.Add("@UsuarioId", barbeiro.UsuarioId);

            var query = @"INSERT INTO barbeiros (nome, email, descricao, url_foto, usuario_id)
                          VALUES (@Nome, @Email, @Descricao, @UrlFoto, @UsuarioId);";

            DataBase.Execute(_configuration, query, parameters);
        }

        public List<AgendamentoMap> ListarTodosAgendamentos()
        {
            var query = @"
        SELECT 
            agendamento_id AS AgendamentoId, 
            cliente_id AS ClienteId, 
            barbeiro_id AS BarbeiroId, 
            horario_disponiveis_id AS HorarioDisponiveisId, 
            data_hora_agendamento AS DataHoraAgendamento, 
            status AS Status, 
            observacoes AS Observacoes
        FROM 
            agendamentos";

            var result = DataBase.Execute<AgendamentoMap>(_configuration, query, new { }).ToList();
            return result;
        }


    }
}
 