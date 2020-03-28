using CentralIdentityServer.Model;
using CentralIdentityServer.Persistence.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralIdentityServer.Persistence.MongoDB
{
    public abstract class MangoDBBaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : EntityBase
    {
        #region protected field
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;
        #endregion

        protected MangoDBBaseRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Create(TEntity obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException($"{typeof(TEntity).Name} object is null");
            }
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
            await _dbCollection.InsertOneAsync(obj);
        }

        public Task Delete(Guid id)
        {
            var objectId = new ObjectId(id.ToByteArray());
            return _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("id", objectId));
        }

        public virtual Task Update(TEntity obj)
        { return _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("id", obj.Id), obj); }

        public async Task<TEntity> GetById(Guid id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("id", id);

            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);

            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}
