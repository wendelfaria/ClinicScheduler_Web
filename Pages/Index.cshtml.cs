using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicScheduler_Web.Data;

namespace ClinicScheduler_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public int TotalPacientes { get; set; }
        public int TotalProfissionais { get; set; }
        public int TotalAgendas { get; set; }
        public int TotalAgendamentos { get; set; }

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Login");

            TotalPacientes = _db.Pacientes.Count();
            TotalProfissionais = _db.Profissionais.Count();
            TotalAgendas = _db.Agendas.Count();
            TotalAgendamentos = _db.Agendamentos.Count();

            return Page();
        }
    }
}