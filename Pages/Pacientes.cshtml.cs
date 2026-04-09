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
    }
}