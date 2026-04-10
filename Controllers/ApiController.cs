using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicScheduler_Web.Data;

namespace ClinicScheduler_Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("agendas")]
        public IActionResult GetAgendas(int profissionalId)
        {
            var agendas = _db.Agendas
                .Where(a => a.ProfissionalId == profissionalId)
                .OrderByDescending(a => a.Data)
                .Select(a => new { a.Id, Data = a.Data.ToString("dd/MM/yyyy") })
                .ToList();

            return Ok(agendas);
        }

        [HttpGet("horarios")]
        public IActionResult GetHorarios(int agendaId)
        {
            var horarios = _db.HorariosAgenda
                .Where(h => h.AgendaId == agendaId)
                .OrderBy(h => h.Hora)
                .Select(h => new
                {
                    h.Id,
                    Hora = h.Hora.ToString("HH:mm"),
                    h.Status,
                    h.ObservacaoBloqueio,
                    Paciente = _db.Agendamentos
                        .Where(a => a.HorarioId == h.Id)
                        .Select(a => a.Paciente!.Nome)
                        .FirstOrDefault()
                })
                .ToList();

            return Ok(horarios);
        }
    }
}