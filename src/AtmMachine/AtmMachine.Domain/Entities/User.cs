using AtmMachine.Domain.ValueObjects.Results;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AtmMachine.Domain.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public static User Create(string name, string surname) => new User(name, surname);

        public User()
        {
        }

        private User(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
        }

        public List<Account> GetAccounts() => new List<Account>();

        public OpenAccountResult OpenAccount(decimal initialDeposit)
        {
            Account newAccount = Account.Create(Id);
            Movement initialMovement = newAccount.RegisterMovement(initialDeposit, "Initial Deposit");
            return OpenAccountResult.Create(newAccount, initialMovement);
        }
    }
}
