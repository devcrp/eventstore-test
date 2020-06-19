using AtmMachine.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Tests
{
    public class MovementTests
    {
        [Test]
        public void Create_Movement_Should_Return_Created_Movement()
        {
            var fakeAccountId = Guid.NewGuid();
            Movement newMovement = Movement.Create(fakeAccountId, 100, "Test movement");
            Assert.AreEqual(fakeAccountId, newMovement.AccountId);
            Assert.AreEqual(100, newMovement.Amount);
            Assert.AreEqual("Test movement", newMovement.Subject);
            Assert.AreNotEqual(Guid.Empty, newMovement.Id);
        }
    }
}
