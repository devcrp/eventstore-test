using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain
{
    public interface IRepository
    {
        bool Insert<T>(T entity);

        T Get<T>(Guid id);

        void Persist();
    }
}
