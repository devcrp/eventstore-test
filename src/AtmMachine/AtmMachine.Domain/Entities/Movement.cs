using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Entities
{
    public class Movement
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; }
        public decimal Amount { get; }
        public string Subject { get; }
        public DateTime DateUtc { get; }

        public static Movement Create(Guid accountId, decimal amount, string subject) => new Movement(accountId, amount, subject);

        private Movement(Guid accountId, decimal amount, string subject)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Amount = amount;
            Subject = subject;
            DateUtc = DateTime.UtcNow;
        }
    }
}
