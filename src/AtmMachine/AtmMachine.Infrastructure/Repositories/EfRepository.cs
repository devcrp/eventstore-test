using AtmMachine.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Infrastructure.Repositories
{
    public class EfRepository : IRepository
    {
        public T Get<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
