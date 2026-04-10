using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicScheduler_Web.Data;
using ClinicScheduler_Web.Models;

namespace ClinicScheduler_Web.Pages
{
    public class AgendasModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Profissional> Profissionais { get; set; } = new();
        public List<Agenda> Agendas { get; set; } = new();
        public string Mensagem { get; set; } = string.Empty;

        public AgendasModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            Agendas = _db.Agendas.Include(a => a.Profissional).OrderByDescending(a => a.Data).ToList();
            return Page();
        }

        public IActionResult OnPost(int profissionalId, DateOnly data, TimeOnly horaInicio,
            TimeOnly horaFim, int intervaloMinutos, bool temAlmoco, int duracaoAlmoco)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            if (profissionalId == 0)
                return Redirect("/Agendas");

            if (horaFim <= horaInicio)
            {
                Mensagem = "Horário de fim deve ser maior que o início.";
                Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
                Agendas = _db.Agendas.Include(a => a.Profissional).OrderByDescending(a => a.Data).ToList();
                return Page();
            }

            var duplicada = _db.Agendas.Any(a => a.ProfissionalId == profissionalId && a.Data == data);
            if (duplicada)
            {
                Mensagem = "Já existe uma agenda para este profissional nesta data.";
                Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
                Agendas = _db.Agendas.Include(a => a.Profissional).OrderByDescending(a => a.Data).ToList();
                return Page();
            }

            var agenda = new Agenda
            {
                ProfissionalId = profissionalId,
                Data = data,
                HoraInicio = horaInicio,
                HoraFim = horaFim,
                IntervaloMinutos = intervaloMinutos,
                TemAlmoco = temAlmoco,
                DuracaoAlmoco = temAlmoco ? duracaoAlmoco : 0
            };

            _db.Agendas.Add(agenda);
            _db.SaveChanges();

            // Gerar horários automaticamente
            var hora = horaInicio;
            var meiodia = TimeOnly.FromTimeSpan(TimeSpan.FromHours(12));
            var fimAlmoco = meiodia.AddMinutes(duracaoAlmoco);

            while (hora < horaFim)
            {
                if (temAlmoco && hora >= meiodia && hora < fimAlmoco)
                {
                    hora = hora.AddMinutes(intervaloMinutos);
                    continue;
                }

                _db.HorariosAgenda.Add(new HorarioAgenda
                {
                    AgendaId = agenda.Id,
                    Hora = hora,
                    Status = "disponivel",
                    ObservacaoBloqueio = string.Empty
                });

                hora = hora.AddMinutes(intervaloMinutos);
            }

            _db.SaveChanges();
            Mensagem = "Agenda criada com sucesso.";
            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            Agendas = _db.Agendas.Include(a => a.Profissional).OrderByDescending(a => a.Data).ToList();
            return Page();
        }
        public IActionResult OnPostExcluir(int agendaId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var agenda = _db.Agendas.FirstOrDefault(a => a.Id == agendaId);
            if (agenda == null)
                return Redirect("/Agendas");

            var horarios = _db.HorariosAgenda.Where(h => h.AgendaId == agendaId).ToList();
            var horarioIds = horarios.Select(h => h.Id).ToList();
            var agendamentos = _db.Agendamentos.Where(a => horarioIds.Contains(a.HorarioId)).ToList();

            _db.Agendamentos.RemoveRange(agendamentos);
            _db.HorariosAgenda.RemoveRange(horarios);
            _db.Agendas.Remove(agenda);
            _db.SaveChanges();

            return Redirect("/Agendas");
        }
    }
}