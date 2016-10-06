using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topics.Core.Models;
using Topics.Data.Entities;

namespace Topics.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add<T>(T obj);

        void AddUserCredentials<T>(T userCredentials);

        void Change<T>(int id, T obj);

        ICollection<T> GetAll<T>();

        T GetByName<T>(string name);

        T GetOne<T>(int id);

        T GetUserCredentialsByUserID<T>(int id);
    }
}
