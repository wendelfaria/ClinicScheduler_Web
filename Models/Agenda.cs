namespace ClinicScheduler_Web.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public int IntervaloMinutos { get; set; }
        public bool TemAlmoco { get; set; }
        public int DuracaoAlmoco { get; set; }

        public int ProfissionalId { get; set; }
        public Profissional? Profissional { get; set; }
    }
}