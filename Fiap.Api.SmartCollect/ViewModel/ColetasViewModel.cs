namespace Fiap.Api.Coletas.ViewModel
{
    public class ColetasViewModel
    {

        public long Id { get; set; }
        public DateTime DataColeta { get; set; }
        public TimeSpan HoraColeta { get; set; }
        public string? TipoResiduo { get; set; }
        public string? Endereco { get; set; }

    }
}
