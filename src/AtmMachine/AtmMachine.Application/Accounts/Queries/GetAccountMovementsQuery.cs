using AtmMachine.Domain;
using AtmMachine.Domain.Entities;
using AtmMachine.Domain.Services;
using AtmMachine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Application.Accounts.Queries
{
    public class GetAccountMovementsQuery
    {
        private readonly IEventRepository _eventRepository;

        public GetAccountMovementsQuery(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;
        }

        public Task<List<Event<Movement>>> Get(Guid accountId)
        {
            return _eventRepository.GetEventsAsync<Movement>(nameof(Account), accountId);
        }
    }
}
