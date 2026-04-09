namespace ClinicScheduler_Web.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;

        public int HorarioId { get; set; }
        public HorarioAgenda? Horario { get; set; }

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
    }
}