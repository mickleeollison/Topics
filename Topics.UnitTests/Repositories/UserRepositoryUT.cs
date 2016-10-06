using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using Topics.Core.Models;
using Topics.Data.DAL.Interfaces;
using Topics.Data.Entities;
using Topics.Data.Mappers;
using Topics.Data.Repositories;
using Topics.UnitTests.Helpers;
using Xunit;

namespace Topics.UnitTests.Repositories
{
    public class UserRepositoryUT
    {
        private Mock<ITopicsContext> _db;
        private DbSetHelper _helper;
        private DbSetHelper _helperCredentials;
        private UserRepository _sut;
        private List<User> _UserList;
        private List<UserCredentials> _UserListCredentials;
        private Mock<DbSet<User>> _UserSet;
        private Mock<DbSet<UserCredentials>> _UserSetCredentials;

        public UserRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ITopicsContext>();
            _sut = new UserRepository(_db.Object);

            User User1 = new User() { UserID = 1, UserName = "User1", RoleID = 1, IsEnabled = true };
            User User2 = new User() { UserID = 2, UserName = "User2", RoleID = 2, IsEnabled = false };
            _UserList = new List<User>() { User1, User2 };

            _UserSet = _helper.GetDbSet(_UserList);
            _db.Setup(c => c.Users).Returns(_UserSet.Object);

            _helperCredentials = new DbSetHelper();
            UserCredentials UserCred1 = new UserCredentials() { UserID = 1, PasswordHash = "User1", Salt = "1" };
            UserCredentials UserCred2 = new UserCredentials() { UserID = 2, PasswordHash = "User2", Salt = "2" };
            _UserListCredentials = new List<UserCredentials>() { UserCred1, UserCred2 };

            _UserSetCredentials = _helper.GetDbSet(_UserListCredentials);
            _db.Setup(c => c.UserCredentials).Returns(_UserSetCredentials.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void AddUser_Test()
        {
            _db.Setup(c => c.Users.Add(It.IsAny<User>()))
                .Callback((User user) => _UserList.Add(user));
            _sut.Add<UserDTO>(new UserDTO());
            Assert.Equal(3, _UserList.Count);
        }

        [Fact]
        public void AddUserCredentials_Test()
        {
            _db.Setup(c => c.UserCredentials.Add(It.IsAny<UserCredentials>()))
                .Callback((UserCredentials userCred) => _UserListCredentials.Add(userCred));
            _sut.AddUserCredentials<UserCredentialsDTO>(new UserCredentialsDTO());
            Assert.Equal(3, _UserListCredentials.Count);
        }

        [Fact]
        public void GetAll_UserDTO_Valid()
        {
            ICollection<UserDTO> actual = _sut.GetAll<UserDTO>();
            Assert.Equal(_UserList.Count, actual.Count);
        }

        [Fact]
        public void GetUserById_Test()
        {
            UserDTO actual = _sut.GetOne<UserDTO>(1);
            Assert.Equal("User1", actual.UserName);
        }

        [Fact]
        public void GetUserByName_Test()
        {
            UserDTO actual = _sut.GetByName<UserDTO>("User2");
            Assert.Equal("User2", actual.UserName);
        }

        [Fact]
        public void GetUserCredentialsByUserId_Test()
        {
            UserCredentialsDTO actual = _sut.GetUserCredentialsByUserID<UserCredentialsDTO>(1);
            Assert.Equal("User1", actual.PasswordHash);
        }
    }
}
