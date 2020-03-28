using MongoDB.Driver;
using System;

namespace CentralIdentityServer.Persistence.MongoDB
{
    public class MongoSettings
    {
        #region public auto-implemented property
        public string Connection { get; set; }

        public string DatabaseName { get; set; }
        #endregion
    }
}
