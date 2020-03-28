using MongoDB.Driver;
using System;

namespace CentralIdentityServer.Persistence.MongoDB
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
