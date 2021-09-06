using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _database;
        public CepImplementation(MyContext Context) : base(Context)
        {
            _database = Context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _database.Include(c => c.Municipio)
                                  .ThenInclude(m => m.Uf)
                                  .SingleOrDefaultAsync(c => c.Cep.Equals(cep));
        }
    }
}
