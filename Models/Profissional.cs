namespace ClinicScheduler_Web.Models
{
    public class Profissional
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Especialidade { get; set; } = string.Empty;
    }
}