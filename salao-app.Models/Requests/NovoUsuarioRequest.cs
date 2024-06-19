namespace salao_app.Models.Requests
{
    public class NovoUsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Tipo { get; set; }

        public string Senha { get; set; }
    }
}
