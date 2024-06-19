using salao_app.Business.Interfaces;
using salao_app.Models.Dto;
using salao_app.Models.Requests;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Maps;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using carvao_app.Repository.Helper;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Enum;
using salao_app.Repository.Services;
using salao_app.Repository.Conexao;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace salao_app.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IBarbeiroRepository _barbeiroRepository;
        private readonly IConfiguration _configuration; 



        public UsuarioService(IUsuarioRepository repository, IClienteRepository clienteRepository, IBarbeiroRepository barbeiroRepository, IConfiguration configuration)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _barbeiroRepository = barbeiroRepository;
            _configuration = configuration; // Adicionado

        }

        public void CadastroUsuario(NovoUsuarioRequest request)
        {
            var usuario = new UsuarioMap
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = Cripto.Encrypt(request.Senha),
                DataRegistro = DateTime.Now,
                PapelId = request.Tipo
            };

            _repository.CadastroUsuario(usuario);

            if (request.Tipo == (int)TipoUsuario.Cliente) 
            {
                var cliente = new ClienteMap
                {
                    nome = request.Nome,
                    usuarioId = usuario.Id,
                    email = request.Email
                };

                _clienteRepository.CriarCliente(cliente);
            }
        }


        public UsuarioDto Login(string email, string senha)
        {
            var usuario = _repository.Login(email, senha);
            if (usuario == null) return null;

            try
            {
                return new UsuarioDto
                {
                    UsuarioId = usuario.Id,
                    PapelId = usuario.PapelId,
                    ClienteId = usuario.Cliente?.clienteId,
                    Nome = usuario.Nome,
                    BarbeiroId = usuario.BarbeiroId, 
                    Token = GenerateJwtToken(new UsuarioDto { UsuarioId = usuario.Id, PapelId = usuario.PapelId })
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao mapear UsuarioDto: {ex.Message}");
                throw;
            }
        }




        public object BuscarTiposUsuarios()
        {
            return _repository.BuscarTiposUsuarios();
        }
        public string GenerateJwtToken(UsuarioDto usuario)
        {
            var key = "sua-chave-secreta-super-segura-de-32-caracteres-ou-mais";
            if (key.Length < 32)
            {
                throw new ArgumentException("A chave secreta deve ter pelo menos 32 caracteres.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.PapelId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public UsuarioDto BuscarUsuarioId(int id)
        {
            var usuario = _repository.BuscarUsuarioId(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                UsuarioId = usuario.Id,
                PapelId = usuario.PapelId
            };
        }
    }
}
