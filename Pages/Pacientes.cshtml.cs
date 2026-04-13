using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicScheduler_Web.Data;
using ClinicScheduler_Web.Models;

namespace ClinicScheduler_Web.Pages
{
    public class PacientesModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Paciente> Pacientes { get; set; } = new();
        public string Mensagem { get; set; } = string.Empty;

        public PacientesModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
            return Page();
        }

        public IActionResult OnPost(string nome, DateTime dataNascimento, string contato, string observacao)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var duplicado = _db.Pacientes.Any(p => p.Nome == nome);
            if (duplicado)
            {
                Mensagem = "Paciente já cadastrado.";
                Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
                return Page();
            }

            _db.Pacientes.Add(new Paciente
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                Contato = contato ?? string.Empty,
                Observacao = observacao ?? string.Empty
            });

            _db.SaveChanges();
            Mensagem = "Paciente cadastrado com sucesso.";
            Pacientes = _db.Pacientes.OrderBy(p => p.Nome).ToList();
            return Page();
        }

        public IActionResult OnPostExcluir(int pacienteId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var paciente = _db.Pacientes.FirstOrDefault(p => p.Id == pacienteId);
            if (paciente == null)
                return Redirect("/Pacientes");

            var agendamentos = _db.Agendamentos.Where(a => a.PacienteId == pacienteId).ToList();
            foreach (var a in agendamentos)
            {
                var horario = _db.HorariosAgenda.FirstOrDefault(h => h.Id == a.HorarioId);
                if (horario != null)
                    horario.Status = "disponivel";
            }

            _db.Agendamentos.RemoveRange(agendamentos);
            _db.Pacientes.Remove(paciente);
            _db.SaveChanges();

            return Redirect("/Pacientes");
        }
    }
}