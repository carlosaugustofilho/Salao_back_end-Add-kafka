using carvao_app.Repository.Helper;
using carvao_app.Repository.Maps;
using Dapper;
using Microsoft.Extensions.Configuration;
using salao_app.Repository.Conexao;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Maps;

namespace salao_app.Repository.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CadastroUsuario(UsuarioMap usuario)
        {
            // Verificar se o usuário já existe com o mesmo email
            var parametersCheck = new DynamicParameters();
            parametersCheck.Add("@Email", usuario.Email);

            var queryCheck = @"SELECT COUNT(1) FROM usuarios WHERE email = @Email";
            var userExists = DataBase.Execute<int>(_configuration, queryCheck, parametersCheck).FirstOrDefault() > 0;

            if (userExists)
            {
                throw new Exception("Já existe um usuário registrado com este email.");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Nome", usuario.Nome);
            parameters.Add("@Email", usuario.Email);
            parameters.Add("@Senha", usuario.Senha);
            parameters.Add("@papel_id", usuario.PapelId);
            parameters.Add("@DataRegistro", usuario.DataRegistro);

            var queryUsuario = @"INSERT INTO Usuarios (nome, email, senha, papel_id, data_registro)
                         VALUES (@Nome, @Email, @senha, @papel_id, @DataRegistro);
                         SELECT LAST_INSERT_ID();";

            var usuarioId = DataBase.Execute<int>(_configuration, queryUsuario, parameters).FirstOrDefault();
            usuario.Id = usuarioId;

            if (usuario.PapelId == 1)
            {
                var parametersCliente = new DynamicParameters();
                parametersCliente.Add("@Nome", usuario.Nome);
                parametersCliente.Add("@UsuarioId", usuarioId);
                parametersCliente.Add("@Email", usuario.Email);

                var queryCliente = @"INSERT INTO Cliente (nome, usuario_id, email)
                             VALUES (@Nome, @UsuarioId, @Email)";

                DataBase.Execute(_configuration, queryCliente, parametersCliente);
            }
        }


        public object BuscarTiposUsuarios()
        {
            return DataBase.Execute<TipoUsuarioMap>(_configuration, "select * from tipo_usuario", new()).ToList();
        }

        //public UsuarioMap Login(string email, string senha)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@Email", email);

        //    var query = "SELECT * FROM usuarios WHERE email = @Email";
        //    var usuario = DataBase.Execute<UsuarioMap>(_configuration, query, parameters).FirstOrDefault()
        //     ?? throw new Exception("Usuário ou senha inválido.");

        //    //Console.WriteLine($"Usuário encontrado: {usuario.Nome}");
        //    //Console.WriteLine($"Senha armazenada (criptografada): {usuario.Senha}");

        //    //// Descriptografe a senha
        //    //string senhaDescriptografada = null;
        //    //if (!string.IsNullOrEmpty(usuario.Senha))
        //    //{
        //    //    senhaDescriptografada = Cripto.Decrypt(usuario.Senha);
        //    //    Console.WriteLine($"Senha descriptografada: {senhaDescriptografada}");
        //    //}

        //    //if (senhaDescriptografada == null || senhaDescriptografada != senha)
        //    //{
        //    //    Console.WriteLine("Senha incorreta.");
        //    //    throw new Exception("senha inválida.");
        //    //}

        //    return usuario;
        //}

        public UsuarioMap Login(string email, string senha)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            var query = @"
                SELECT 
                    u.usuario_id AS Id, 
                    u.nome, 
                    u.email, 
                    u.senha, 
                    u.papel_id AS PapelId, 
                    u.data_registro AS DataRegistro 
                FROM usuarios u
                WHERE u.email = @Email";
            var usuario = DataBase.Execute<UsuarioMap>(_configuration, query, parameters).FirstOrDefault()
                ?? throw new Exception("Usuário ou senha inválido.");

            if (Cripto.Decrypt(usuario.Senha) != senha)
            {
                throw new Exception("senha inválida.");
            }

            var clienteQuery = "SELECT cliente_id AS ClienteId, nome, usuario_id AS UsuarioId, email FROM cliente WHERE usuario_id = @Id";
            var cliente = DataBase.Execute<ClienteMap>(_configuration, clienteQuery, new { Id = usuario.Id }).FirstOrDefault();

            var barbeiroQuery = "SELECT barbeiro_id AS BarbeiroId, usuario_id AS UsuarioId, email, nome, descricao, url_foto FROM barbeiros WHERE usuario_id = @Id";
            var barbeiro = DataBase.Execute<BarbeiroMap>(_configuration, barbeiroQuery, new { Id = usuario.Id }).FirstOrDefault();

            usuario.Cliente = cliente;
            usuario.Barbeiro = barbeiro;

            return usuario;
        }

        public UsuarioMap BuscarUsuarioId(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var query = "SELECT * FROM Usuarios WHERE Id = @Id";
            return DataBase.Execute<UsuarioMap>(_configuration, query, parameters).FirstOrDefault();
        }
    }
}
