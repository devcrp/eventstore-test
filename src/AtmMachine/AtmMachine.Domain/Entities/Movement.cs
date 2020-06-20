using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Entities
{
    public class Movement : IEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Subject { get; set; }
        public DateTime DateUtc { get; set; }

        public static Movement Create(Guid accountId, decimal amount, string subject) => new Movement(accountId, amount, subject);

        public Movement()
        {

        }

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
