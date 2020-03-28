using CentralIdentityServer.Model;
using CentralIdentityServer.Persistence.Contract;
using System;
using System.Collections.Generic;

namespace CentralIdentityServer.Persistence.MongoDB
{
    public class UserRepositoryMongoDB : MangoDBBaseRepository<User>, IBaseRepository<User>
    {
        public UserRepositoryMongoDB(IMongoDBContext context) : base(context) { }
    }
}
