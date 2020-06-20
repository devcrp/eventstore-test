using AtmMachine.Domain;
using AtmMachine.Infrastructure.Contexts.EfContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmMachine.Infrastructure.Repositories
{
    public class EfRepository : IDbRepository
    {
        private readonly EfContext _context;

        public EfRepository(EfContext context)
        {
            this._context = context;
        }

        public T Get<T>(Guid id) where T : class, IEntity
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public void Insert<T>(T entity) where T : class, IEntity
        {
            _context.Set<T>().Add(entity);
        }

        public void Persist()
        {
            _context.SaveChanges();
        }
    }
}
