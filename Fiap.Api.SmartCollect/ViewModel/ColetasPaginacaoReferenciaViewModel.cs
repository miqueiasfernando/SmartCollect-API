namespace Fiap.Api.Coletas.ViewModel
{
    public class ColetasPaginacaoReferenciaViewModel
    {
        public IEnumerable<ColetasViewModel> Coletas { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public long NextRef { get; set; }
        public string PreviusPageUrl => $"/api/coleta?referencia={Ref}&tamanho={PageSize} ";
        public string NextPageUrl => (Ref < NextRef) ? $"/api/coleta?referencia={Ref}&tamanho={PageSize}" : "";

    }
}
