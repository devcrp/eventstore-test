using AtmMachine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Domain
{
    public interface IEventRepository
    {
        Task AddEventAsync<T>(string stream, string @event, T entity) where T : class, IEntity;

        Task<List<Event<T>>> GetEventsAsync<T>(string stream);
    }
}
