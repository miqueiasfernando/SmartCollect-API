using Fiap.Api.Coletas.Models;

namespace Fiap.Api.Coletas.Services
{
    public interface IColetasService
    {
        IEnumerable<ColetasModel> ListarColetas();
        IEnumerable<ColetasModel> ListarColetasReferencia(long ultimoId = 0, int tamanho = 10);
        ColetasModel ObtercoletaPorId(long id);
        void Atualizar(ColetasModel coleta);
        void Criar(ColetasModel coleta);
        void Excluir(long id);
    }
}
