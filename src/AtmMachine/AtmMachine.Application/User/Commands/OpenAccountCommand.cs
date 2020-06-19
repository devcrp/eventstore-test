using AtmMachine.Domain;
using AtmMachine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmMachine.Application.Services
{
    public class OpenAccountCommand
    {
        private readonly IRepository _repository;

        public OpenAccountCommand(IRepository repository)
        {
            this._repository = repository;
        }

        public void OpenAccount(Guid userId, decimal initialDeposit)
        {
            User user = _repository.Get<User>(userId);
            Account newAccount = user.OpenAccount(initialDeposit);

            _repository.Insert(newAccount);
            _repository.Insert(newAccount.Movements.Single());

            _repository.Persist();
        }
    }
}
