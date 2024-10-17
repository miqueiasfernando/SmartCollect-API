namespace Fiap.Api.Coletas.Models
{
    public class ColetasModel
    {

        public long Id { get; set; }
        public DateTime DataColeta { get; set; }
        public TimeSpan HoraColeta { get; set; }
        public string? TipoResiduo { get; set; }
        public string? Endereco { get; set; }

    }
}
