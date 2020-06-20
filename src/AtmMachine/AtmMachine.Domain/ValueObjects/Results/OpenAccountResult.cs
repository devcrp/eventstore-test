using AtmMachine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.ValueObjects.Results
{
    public class OpenAccountResult
    {
        public Account Account { get; }
        public Movement Movement { get; }

        public static OpenAccountResult Create(Account account, Movement movement) => new OpenAccountResult(account, movement);

        private OpenAccountResult(Account account, Movement movement)
        {
            Account = account;
            Movement = movement;
        }
    }
}
