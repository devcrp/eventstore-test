using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain
{
    public interface IDbRepository
    {
        void Insert<T>(T entity) where T : class, IEntity;

        T Get<T>(Guid id) where T : class, IEntity;

        void Persist();
    }
}
