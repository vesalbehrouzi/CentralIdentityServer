using CentralIdentityServer.Model;
using CentralIdentityServer.Persistence.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using Xunit;

namespace CentralIdentityServer.Test.Unit
{
    public class MongoDBContextTests
    {
        #region private field
        private Mock<IOptions<MongoSettings>> mockOptions;
        private Mock<IMongoDatabase> mockDB;
        private Mock<IMongoClient> mockClient;
        #endregion

        public MongoDBContextTests()
        {
            mockOptions = new Mock<IOptions<MongoSettings>>();
            mockDB = new Mock<IMongoDatabase>();
            mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public void MongoDBContext_Constructor_Success()
        {
            var settings = new MongoSettings() { Connection = "mongodb://tes123", DatabaseName = "TestDB" };

            mockOptions.Setup(s => s.Value).Returns(settings);
            mockClient.Setup(c => c
            .GetDatabase(mockOptions.Object.Value.DatabaseName, null))
                .Returns(mockDB.Object);


            var context = new MongoDBContext(mockOptions.Object);


            Assert.NotNull(context);
        }

        [Fact]
        public void MongoDBContext_GetCollection_NameEmpty_Failure()
        {
            var settings = new MongoSettings() { Connection = "mongodb://tes123", DatabaseName = "TestDB" };

            mockOptions.Setup(s => s.Value).Returns(settings);
            mockClient.Setup(c => c
            .GetDatabase(mockOptions.Object.Value.DatabaseName, null))
                .Returns(mockDB.Object);

            Action act = () =>
            {
                var context = new MongoDBContext(mockOptions.Object);
                var myCollection = context.GetCollection<User>(string.Empty);
            };

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void MongoDBContext_GetCollection_ValidName_Success()
        {
            var settings = new MongoSettings() { Connection = "mongodb://tes123", DatabaseName = "TestDB" };

            mockOptions.Setup(s => s.Value).Returns(settings);

            mockClient.Setup(c => c.GetDatabase(mockOptions.Object.Value.DatabaseName, null)).Returns(mockDB.Object);


            var context = new MongoDBContext(mockOptions.Object);
            var myCollection = context.GetCollection<User>(nameof(User));


            Assert.NotNull(myCollection);
        }
    }
}
