using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Services
{
    public static class StreamNameProvider
    {
        public static string Get<T>(T entity) where T : IEntity
        {
            return $"{entity.GetType().Name.ToLower()}-{entity.Id}";
        }

        public static string Get<T>(Guid id) where T : IEntity
        {
            return $"{typeof(T).Name.ToLower()}-{id}";
        }
    }
}
