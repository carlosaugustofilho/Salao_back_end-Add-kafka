using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salao_app.Repository.Maps;

public class HorariosTrabalhoBarbeiroMap
{
    public int Id { get; set; }
    public int BarbeiroId { get; set; }
   // public DiaSemanaEnum DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }

    // Relacionamento com a tabela Barbeiros
    public BarbeiroMap Barbeiro { get; set; }
    public ClienteMap clienteMAp { get; set; }
}