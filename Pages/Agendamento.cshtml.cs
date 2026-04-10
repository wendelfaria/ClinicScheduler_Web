using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClinicScheduler_Web.Data;
using ClinicScheduler_Web.Models;

namespace ClinicScheduler_Web.Pages
{
    public class AgendamentoModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Profissional> Profissionais { get; set; } = new();
        public List<Paciente> Pacientes { get; set; } = new();
        public string Mensagem { get; set; } = string.Empty;

        public AgendamentoModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
            return Page();
        }

        public IActionResult OnPost(int horarioId, int pacienteId, int agendaId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var horario = _db.HorariosAgenda.FirstOrDefault(h => h.Id == horarioId);
            if (horario == null || horario.Status != "disponivel")
            {
                Mensagem = "Horário indisponível.";
                Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
                Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
                return Page();
            }

            var jaAgendado = _db.Agendamentos.Any(a =>
                a.HorarioId == horarioId && a.PacienteId == pacienteId);
            if (jaAgendado)
            {
                Mensagem = "Paciente já agendado neste horário.";
                Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
                Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
                return Page();
            }

            _db.Agendamentos.Add(new Agendamento
            {
                HorarioId = horarioId,
                PacienteId = pacienteId,
                Status = "agendado"
            });

            horario.Status = "agendado";
            _db.SaveChanges();

            Mensagem = "Paciente agendado com sucesso.";
            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
            return Page();
        }
        public IActionResult OnPostAlterarStatus(int horarioId, string novoStatus, string motivoBloqueio)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var horario = _db.HorariosAgenda.FirstOrDefault(h => h.Id == horarioId);
            if (horario == null)
                return Redirect("/Agendamento");

            if (novoStatus == "disponivel")
            {
                var agendamento = _db.Agendamentos.FirstOrDefault(a => a.HorarioId == horarioId);
                if (agendamento != null)
                    _db.Agendamentos.Remove(agendamento);

                horario.Status = "disponivel";
                horario.ObservacaoBloqueio = string.Empty;
            }
            else
            {
                horario.Status = novoStatus;
                horario.ObservacaoBloqueio = novoStatus == "bloqueado"
                    ? (string.IsNullOrEmpty(motivoBloqueio) ? "Bloqueado" : motivoBloqueio)
                    : string.Empty;
            }

            _db.SaveChanges();
            return Redirect("/Agendamento");
        }
    }

}