using salao_app.Repository.Maps;
using System.Collections.Generic;

namespace salao_app.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        void CadastroUsuario(UsuarioMap usuario);
        UsuarioMap Login(string email, string senha);
        UsuarioMap BuscarUsuarioId(int id);
        object BuscarTiposUsuarios();
    }
}
