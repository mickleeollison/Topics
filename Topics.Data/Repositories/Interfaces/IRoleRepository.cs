﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topics.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<T> GetAll<T>();

        T GetOne<T>(int id);
    }
}
