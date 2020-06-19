using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; }
        public List<Movement> Movements { get; private set; } = new List<Movement>();

        public static Account Create(Guid userId) => new Account(userId);

        private Account(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }

        public Movement RegisterMovement(decimal amount, string subject)
        {
            Movement movement = Movement.Create(Id, amount, subject);
            Movements.Add(movement);
            return movement;
        }
    }
}
