using Fiap.Api.Coletas.Data.Contexts;
using Fiap.Api.Coletas.Data.Repository;
using Fiap.Api.Coletas.Exception;
using Fiap.Api.Coletas.Models;

namespace Fiap.Api.Coletas.Services
{
    public class ColetasService : IColetasService
    {
        private readonly IColetasRepository _repository;

        public ColetasService(IColetasRepository repository)
        {
            _repository = repository;
        }

        public void Atualizar(ColetasModel coleta) => _repository.Update(coleta);

        public void Criar(ColetasModel coleta) => _repository.add(coleta);
        

        public void Excluir(long id)
        {
            var coleta = _repository.GetById(id);
            if (coleta != null)
            {
                _repository.Delete(coleta);
            }
        }
        

        public IEnumerable<ColetasModel> ListarColetas() => _repository.GetAll();
        

        public ColetasModel ObtercoletaPorId(long id)
        {
            var coleta = _repository.GetById(id);
            if (coleta == null)
            {
                throw new NotFoundException($"coleta com ID {id} não encontrado.");
            }
            return coleta;
        }

        public IEnumerable<ColetasModel> ListarColetasReferencia(long ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }
        
    }
}
