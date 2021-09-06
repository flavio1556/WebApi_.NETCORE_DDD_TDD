using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {
        //private DbSet<MunicipioEntity> _dataset;
        public MunicipioImplementation(MyContext Context) : base(Context)
        {
            _dataset = Context.Set<MunicipioEntity>();
        }

        public async Task<MunicipioEntity> GetCompleteByID(Guid id)
        {
            return await _dataset.Include(m => m.Uf)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MunicipioEntity> GetCompletoByIBGE(int codIBGE)
        {
            return await _dataset.Include(m => m.Uf)
                                 .FirstOrDefaultAsync(m => m.CodIBGE.Equals(codIBGE));
        }
    }
}
