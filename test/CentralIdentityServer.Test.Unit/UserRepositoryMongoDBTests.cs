using CentralIdentityServer.Model;
using CentralIdentityServer.Persistence.MongoDB;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CentralIdentityServer.Test.Unit
{
    public class UserRepositoryMongoDBTests
    {
        #region private field
        private Mock<IMongoCollection<User>> mockCollection;
        private Mock<IMongoDBContext> mockContext;
        private User user;
        private IList<User> users;
        #endregion

        public UserRepositoryMongoDBTests()
        {
            user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "testFirstName",
                LastName = "testLastName",
                UserName = "testUserName",
                EmailAddress = "email@test.net",
                PhoneNumber = "123456789",
                UserGroups = new List<UserGroup> { new UserGroup() { GroupId = Guid.NewGuid(), Title = "Test Group" } }
            };

            users = new List<User> { user };
            mockCollection = new Mock<IMongoCollection<User>>();
            mockContext = new Mock<IMongoDBContext>();
        }

        [Fact]
        public async void UserRepository_CreateNewUser_Valid_Success()
        {
            mockCollection.Setup(op => op.InsertOneAsync(user, null, default(CancellationToken)))
                .Returns(Task.CompletedTask);

            mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(mockCollection.Object);
            var userRepo = new UserRepositoryMongoDB(mockContext.Object);

            await userRepo.Create(user);

            mockCollection.Verify(c => c.InsertOneAsync(user, null, default(CancellationToken)), Times.Once);
        }

        [Fact]
        public void UserRepository_GetUserSync_Valid_Success()
        {
            Mock<IAsyncCursor<User>> userCursor = new Mock<IAsyncCursor<User>>();
            userCursor.Setup(_ => _.Current).Returns(users);
            userCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);


            mockCollection.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<User>>(),
                                                    It.IsAny<FindOptions<User, User>>(),
                                                    It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userCursor.Object));


            mockContext.Setup(c => c.GetCollection<User>(typeof(User).Name)).Returns(mockCollection.Object);

            var userRepo = new UserRepositoryMongoDB(mockContext.Object);


            var result = userRepo.GetAll().Result;

            foreach(User item in result)
            {
                Assert.NotNull(user);
                Assert.Equal(item.FirstName, user.FirstName);
                Assert.Equal(item.LastName, user.LastName);
                break;
            }


            mockCollection.Verify(c => c.FindAsync(It.IsAny<FilterDefinition<User>>(),
                                                   It.IsAny<FindOptions<User>>(),
                                                   It.IsAny<CancellationToken>()),
                                                   Times.Once);
        }
    }
}
