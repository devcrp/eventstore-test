using AtmMachine.Domain;
using AtmMachine.Domain.Constants;
using AtmMachine.Domain.Entities;
using AtmMachine.Domain.Services;
using AtmMachine.Domain.ValueObjects.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Application.Users.Commands
{
    public class OpenAccountCommand
    {
        private readonly IDbRepository _dbRepository;
        private readonly IEventRepository _eventRepository;

        public OpenAccountCommand(IDbRepository dbRepository, IEventRepository eventRepository)
        {
            this._dbRepository = dbRepository;
            this._eventRepository = eventRepository;
        }

        public async Task<Guid> RunAsync(Guid userId, decimal initialDeposit)
        {
            User user = _dbRepository.Get<User>(userId);
            OpenAccountResult result = user.OpenAccount(initialDeposit);

            _dbRepository.Insert(result.Account);
            _dbRepository.Persist();

            await _eventRepository.AddEventAsync(nameof(Account), 
                                            result.Account.Id,
                                            result.Movement.Amount >= 0 ? EventType.Deposit : EventType.Withdraw,
                                            result.Movement);

            return result.Account.Id;
        }
    }
}
