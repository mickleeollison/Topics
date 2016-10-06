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
    public class RoleRepositoryUT
    {
        private Mock<ITopicsContext> _db;
        private DbSetHelper _helper;
        private List<Role> _RoleList;
        private Mock<DbSet<Role>> _RoleSet;
        private RoleRepository _sut;

        public RoleRepositoryUT()
        {
            _helper = new DbSetHelper();
            _db = new Mock<ITopicsContext>();
            _sut = new RoleRepository(_db.Object);

            Role role1 = new Role() { RoleID = 1, Name = "role1" };
            Role role2 = new Role() { RoleID = 2, Name = "role2" };
            _RoleList = new List<Role>() { role1, role2 };

            _RoleSet = _helper.GetDbSet(_RoleList);
            _db.Setup(c => c.Roles).Returns(_RoleSet.Object);

            AutoMapperConfig.Execute();
        }

        [Fact]
        public void GetAll_RoleDTO_Valid()
        {
            ICollection<RoleDTO> actual = _sut.GetAll<RoleDTO>();
            Assert.Equal(_RoleList.Count, actual.Count);
        }

        [Fact]
        public void GetRoleById_Test()
        {
            RoleDTO actual = _sut.GetOne<RoleDTO>(1);
            Assert.Equal("role1", actual.Name);
        }
    }
}
