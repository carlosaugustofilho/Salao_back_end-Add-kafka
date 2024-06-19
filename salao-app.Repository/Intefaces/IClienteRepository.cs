using salao_app.Repository.Maps;
using SalaoApp.Models;

namespace salao_app.Repository.Intefaces
{
    public interface IClienteRepository
    {
        List<ClienteMap> BuscarClientes(string nome, string email);
        ClienteMap BuscarClientePorId(int id);

        void AgendarHorarioCliente(int clienteId, int barbeiroId, DateTime data, TimeSpan horaInicio, TimeSpan horaFim, int usuarioId);
        void CancelarAgendamento(int agendamentoId);
        List<HorarioDisponivelMap> ObterHorariosDisponiveis(int barbeiroId);

        void AtualizarStatusHorario(int horarioId, bool disponivel);
        void CriarCliente(ClienteMap cliente);
        void AtualizarCliente(ClienteMap cliente);
        void DeletarCliente(int id);
        bool ExistCliente(ClienteMap cliente);
    }
}


