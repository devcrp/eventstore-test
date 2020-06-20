using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public static Account Create(Guid userId) => new Account(userId);

        private Account()
        {
        }

        private Account(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }

        public Movement RegisterMovement(decimal amount, string subject)
        {
            return Movement.Create(Id, amount, subject);
        }
    }
}
