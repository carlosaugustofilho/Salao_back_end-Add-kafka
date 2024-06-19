public class UsuarioDto
{
    public int UsuarioId { get; set; }
    public int PapelId { get; set; }
    public int? ClienteId { get; set; } // Incluindo o clienteId
    public string Nome { get; set; }
    public string Token { get; set; }
    public int? BarbeiroId { get; set; } // Incluindo o barbeiroId
}
