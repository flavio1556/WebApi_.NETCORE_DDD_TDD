using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAnsync(T item);
        Task<T> UpdateAnsync(T item);
        Task<bool> DeleteAnsync(Guid id);
        Task<T> SelectAnsync(Guid id);
        Task<IEnumerable<T>> SelectAnsync();
        Task<bool> ExistAsync(Guid id);
    }
}
