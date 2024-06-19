namespace salao_app.Models.Requests
{
    public class AdicionarHorarioDisponivelRequest
    {
        public int BarbeiroId { get; set; }
        public DateTime Data { get; set; }  // Adicione esta linha
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}
