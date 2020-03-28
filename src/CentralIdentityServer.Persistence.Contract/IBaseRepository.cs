using CentralIdentityServer.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralIdentityServer.Persistence.Contract
{
    public interface IBaseRepository<TEntity>
        where TEntity : EntityBase
    {
        Task Create(TEntity obj);

        Task Update(TEntity obj);

        Task Delete(Guid id);

        Task<TEntity> GetById(Guid id);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
