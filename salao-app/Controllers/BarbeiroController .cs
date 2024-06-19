using Microsoft.AspNetCore.Mvc;
using salao_app.Models.Requests;
using salao_app.Repository.Maps;
using salao_app.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BarbeiroController : ControllerBase
{
    private readonly IBarbeiroService _barbeiroService;

    public BarbeiroController(IBarbeiroService barbeiroService)
    {
        _barbeiroService = barbeiroService;
    }

    [HttpPost("AdicionarHorario")]
    public IActionResult AdicionarHorarioDisponivel([FromBody] AdicionarHorarioDisponivelRequest request)
    {
        try
        {
            _barbeiroService.AdicionarHorarioDisponivel(request.BarbeiroId, request.Data, request.HoraInicio, request.HoraFim);
            return Ok(new { message = "Horário adicionado com sucesso." });  // Alteração aqui
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });  // Alteração aqui
        }
    }


    [HttpGet("ListarHorariosDisponiveis")]
    public IActionResult ListarHorariosDisponiveis(int barbeiroId)
    {
        try
        {
            var horarios = _barbeiroService.ListarHorariosDisponiveis(barbeiroId);
            return Ok(horarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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

    [HttpGet("BuscarBarbeiroPorId/{barbeiroId}")]
    public IActionResult BuscarBarbeiroPorId(int barbeiroId)
    {
        try
        {
            var barbeiro = _barbeiroService.BuscarBarbeiroPorId(barbeiroId);
            if (barbeiro == null)
                return NotFound(new { message = "Barbeiro não encontrado." });

            return Ok(barbeiro);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("ListarTodosAgendamentos")]
    public IActionResult ListarTodosAgendamentos()
    {
        try
        {
            var agendamentos = _barbeiroService.ListarTodosAgendamentos();
            return Ok(agendamentos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


}

