namespace ClinicScheduler_Web.Models
{
    public class HorarioAgenda
    {
        public int Id { get; set; }
        public TimeOnly Hora { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ObservacaoBloqueio { get; set; } = string.Empty;

        public int AgendaId { get; set; }
        public Agenda? Agenda { get; set; }
    }
}