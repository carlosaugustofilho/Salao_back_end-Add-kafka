using salao_app.Repository.Maps;

public class UsuarioMap
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int PapelId { get; set; }
    public DateTime DataRegistro { get; set; }
    public ClienteMap Cliente { get; set; }
    public BarbeiroMap Barbeiro { get; set; } // Propriedade para o objeto Barbeiro
    public int? BarbeiroId => Barbeiro?.BarbeiroId; // Propriedade derivada para o BarbeiroId
}
