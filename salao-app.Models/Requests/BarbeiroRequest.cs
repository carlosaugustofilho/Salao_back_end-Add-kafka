namespace salao_app.Models.Requests
{
    public class BarbeiroRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Descricao { get; set; }
        public string UrlFoto { get; set; }
    }
}
