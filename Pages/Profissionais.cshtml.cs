using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicScheduler_Web.Data;
using ClinicScheduler_Web.Models;

namespace ClinicScheduler_Web.Pages
{
    public class ProfissionaisModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Profissional> Profissionais { get; set; } = new();
        public string Mensagem { get; set; } = string.Empty;

        public ProfissionaisModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            return Page();
        }

        public IActionResult OnPost(string nome, DateTime dataNascimento, string especialidade)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var duplicado = _db.Profissionais.Any(p => p.Nome == nome);
            if (duplicado)
            {
                Mensagem = "Profissional já cadastrado.";
                Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
                return Page();
            }

            _db.Profissionais.Add(new Profissional
            {
                Nome = nome,
                DataNascimento = dataNascimento,
                Especialidade = especialidade
            });

            _db.SaveChanges();
            Mensagem = "Profissional cadastrado com sucesso.";
            Profissionais = _db.Profissionais.OrderBy(p => p.Nome).ToList();
            return Page();
        }

        public IActionResult OnPostExcluir(int profissionalId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            var profissional = _db.Profissionais.FirstOrDefault(p => p.Id == profissionalId);
            if (profissional == null)
                return Redirect("/Profissionais");

            var agendas = _db.Agendas.Where(a => a.ProfissionalId == profissionalId).ToList();
            foreach (var agenda in agendas)
            {
                var horarios = _db.HorariosAgenda.Where(h => h.AgendaId == agenda.Id).ToList();
                var horarioIds = horarios.Select(h => h.Id).ToList();
                var agendamentos = _db.Agendamentos.Where(a => horarioIds.Contains(a.HorarioId)).ToList();
                _db.Agendamentos.RemoveRange(agendamentos);
                _db.HorariosAgenda.RemoveRange(horarios);
            }

            _db.Agendas.RemoveRange(agendas);
            _db.Profissionais.Remove(profissional);
            _db.SaveChanges();

            return Redirect("/Profissionais");
        }
    }
}