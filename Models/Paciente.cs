namespace ClinicScheduler_Web.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Contato { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}