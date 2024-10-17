using Fiap.Api.Coletas.Models;

namespace Fiap.Api.Coletas.Data.Repository
{
    public interface IColetasRepository
    {
        IEnumerable<ColetasModel> GetAll();
        IEnumerable<ColetasModel> GetAllReference(long lastRefenrece, int size);
        ColetasModel GetById(long id);
        void add(ColetasModel coleta);
        void Update(ColetasModel coleta);
        void Delete(ColetasModel coleta);
    }
}
