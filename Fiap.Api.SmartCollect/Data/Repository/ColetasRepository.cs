using Fiap.Api.Coletas.Data.Contexts;
using Fiap.Api.Coletas.Models;
using Fiap.Api.Coletas.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Coletas.Data.Repository
{
    public class ColetasRepository : IColetasRepository
    {
        private readonly DatabaseContext _context;

        public ColetasRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ColetasModel> GetAllReference(long lastRefenrece, int size)
        {
            var Coletas = _context.Coletas
                .Where(c => c.Id > lastRefenrece)
                .OrderBy(c => c.Id)
                .Take(size)
                .AsNoTracking()
                .ToList();

            return Coletas;
        }

        public IEnumerable<ColetasModel> GetAll() => _context.Coletas.ToList();

        public ColetasModel GetById(long id) => _context.Coletas.Find(id);

        public void add(ColetasModel coleta)
        {
            _context.Coletas.Add(coleta);
            _context.SaveChanges();
        }

        public void Update(ColetasModel coleta)
        {
            _context.Coletas.Update(coleta);
            _context.SaveChanges();
        }

        public void Delete(ColetasModel coleta)
        {
            _context.Coletas.Remove(coleta);
            _context.SaveChanges();
        }
        
    }
}
