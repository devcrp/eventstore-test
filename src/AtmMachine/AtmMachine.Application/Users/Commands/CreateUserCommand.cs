using AtmMachine.Domain;
using AtmMachine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Application.Users.Commands
{
    public class CreateUserCommand
    {
        private readonly IDbRepository _repository;

        public CreateUserCommand(IDbRepository repository)
        {
            this._repository = repository;
        }

        public Guid Run(string name, string surname)
        {
            User user = User.Create(name, surname);
            _repository.Insert<User>(user);
            _repository.Persist();

            return user.Id;
        }
    }
}
