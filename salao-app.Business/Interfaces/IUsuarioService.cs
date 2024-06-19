using salao_app.Models.Dto;
using salao_app.Models.Requests;
using salao_app.Repository.Maps;

namespace salao_app.Business.Interfaces
{
    public interface IUsuarioService
    {
        void CadastroUsuario(NovoUsuarioRequest request);
        UsuarioDto Login(string email, string senha);
        string GenerateJwtToken(UsuarioDto usuario);
        UsuarioDto BuscarUsuarioId(int id);
        object BuscarTiposUsuarios();
    }
}
