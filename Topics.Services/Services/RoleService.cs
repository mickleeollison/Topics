using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;
using Topics.Data.Repositories.Interfaces;
using Topics.Services.Services.Interfaces;

namespace Topics.Services.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _RoleRepository;

        public RoleService(IRoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;
        }

        public RoleDTO GetRole(int id)
        {
            return _RoleRepository.GetOne<RoleDTO>(id);
        }

        public ICollection<RoleDTO> GetRoles()
        {
            return _RoleRepository.GetAll<RoleDTO>();
        }
    }
}
