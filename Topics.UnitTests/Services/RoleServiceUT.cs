using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Topics.Core.Models;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services;
using Xunit;

namespace Topics.UnitTests.Services
{
    public class RoleServicesUT
    {
        private Mock<IRoleRepository> _roleRepository;
        private RoleService _sut;

        public RoleServicesUT()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _sut = new RoleService(_roleRepository.Object);
        }

        [Fact]
        public void GetRole_Test()
        {
            RoleDTO expectedRole = new RoleDTO()
            {
                RoleID = 1,
                Name = "role1"
            };
            _roleRepository.Setup(b => b.GetOne<RoleDTO>(It.IsAny<int>())).Returns(expectedRole);
            RoleDTO actualRole = _sut.GetRole(1);
            Assert.Equal(expectedRole, actualRole);
        }

        [Fact]
        public void GetRoles_Test()
        {
            RoleDTO r = null;
            ICollection<RoleDTO> rs = new Collection<RoleDTO>();
            rs.Add(r);
            _roleRepository.Setup(b => b.GetAll<RoleDTO>()).Returns(rs);
            int actual = _sut.GetRoles().Count;
            Assert.Equal(1, actual);
        }
    }
}
