using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using salao_app.Business.Interfaces;
using salao_app.Models.Requests;
using salao_app.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly IBarbeiroService _barbeiroService;

    public UsuarioController(IUsuarioService usuarioService, IBarbeiroService barbeiroService)
    {
        _usuarioService = usuarioService;
        _barbeiroService = barbeiroService;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register([FromBody] NovoUsuarioRequest request)
    {
        try
        {
            _usuarioService.CadastroUsuario(request);
            return Ok(new { message = "Usuário registrado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao registrar usuário: {ex.Message}" });
        }
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _usuarioService.Login(request.Email, request.Senha);

        if (user == null)
            return Unauthorized(new { message = "Usuário ou senha inválidos" });
        return Ok(user);
    }


    [HttpGet]
    [Route("/Usuario/TiposUsuarios")]
    public async Task<IActionResult> GetTiposUsuarios()
    {
        try
        {
            var tipos = _usuarioService.BuscarTiposUsuarios();
            return Ok(tipos);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetUserById(int id)
    {
        var user = _usuarioService.BuscarUsuarioId(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("CriarBarbeiro")]
    public IActionResult CriarBarbeiro([FromBody] BarbeiroRequest request)
    {
        try
        {
            _barbeiroService.CriarBarbeiro(request);
            return Ok(new { message = "Barbeiro criado com sucesso." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
