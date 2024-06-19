using Microsoft.AspNetCore.Mvc;
using salao_app.Models;
using salao_app.Business.Interfaces;
using SalaoApp.Models;
using Microsoft.Extensions.Logging;

namespace salao_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioService _horarioService;
        private readonly ILogger<HorarioController> _logger;
        public HorarioController(IHorarioService horarioService, ILogger<HorarioController> logger)
        {
            _horarioService = horarioService;
            _logger = logger;
        }

        
        [HttpGet]
        [Route("/api/Horario/BuscarTodosHorarios/")]
        public IActionResult BuscarTodosHorarios(DateTime data)
        {
            var horarios = _horarioService.BuscarTodosHorarios(data);
            return Ok(horarios);
        }

        [HttpGet]
        [Route("/api/Horario/BuscarHorarioPorId/")]
        public IActionResult BuscarHorarioPorId(int id)
        {
            var horario = _horarioService.BuscarHorarioPorId(id);
            if (horario == null)
            {
                return NotFound();
            }
            return Ok(horario);
        }

        [HttpDelete]
        [Route("/api/HorarioDisponivel/DeletarHorario/")]
        public IActionResult DeleteHorario(int id)
        {
            var existingHorario = _horarioService.BuscarHorarioPorId(id);
            if (existingHorario == null)
            {
                return NotFound();
            }
            _horarioService.DeletarHorario(id);
            return NoContent();
        }
    }
}
