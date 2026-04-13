using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClinicScheduler_Web.Data;
using System.Security.Cryptography;
using System.Text;

namespace ClinicScheduler_Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;

        public string ErroLogin { get; set; } = string.Empty;

        public LoginModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
                return Redirect("/Index");

            return Page();
        }

        public IActionResult OnPost(string usuario, string senha)
        {
            var hash = GerarHash(senha);
            var user = _db.Usuarios.FirstOrDefault(u => u.NomeUsuario == usuario && u.SenhaHash == hash);

            if (user == null)
            {
                ErroLogin = "Usuário ou senha incorretos.";
                return Page();
            }

            HttpContext.Session.SetString("usuario", user.NomeUsuario);
            HttpContext.Session.SetString("nivelAcesso", user.NivelAcesso);

            return Redirect("/Index");
        }

        private string GerarHash(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}