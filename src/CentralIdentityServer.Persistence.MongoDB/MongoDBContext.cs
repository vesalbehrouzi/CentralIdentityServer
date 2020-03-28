using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace CentralIdentityServer.Persistence.MongoDB
{
    public class MongoDBContext : IMongoDBContext
    {
        public MongoDBContext(IOptions<MongoSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.Connection);
            _db = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        private IMongoDatabase _db { get; set; }

        private MongoClient _mongoClient { get; set; }

        public IMongoCollection<T> GetCollection<T>(string name) { return _db.GetCollection<T>(name); }

        #region public auto-implemented property
        public IClientSessionHandle Session { get; set; }
        #endregion
    }
}
