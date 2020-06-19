using AtmMachine.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Tests
{
    public class AccountTests
    {
        Account _Account;

        [SetUp]
        public void Setup()
        {
            _Account = Account.Create(Guid.NewGuid());
        }

        [Test]
        public void Create_Account_Should_Return_Created_Account()
        {
            var fakeUserId = Guid.NewGuid();
            Account newAccount = Account.Create(fakeUserId);
            Assert.AreEqual(fakeUserId, newAccount.UserId);
            Assert.AreNotEqual(Guid.Empty, newAccount.Id);
        }

        [Test]
        public void Register_Movement_Should_Link_Movement_To_Account()
        {
            Movement newMovement = _Account.RegisterMovement(150, "Test1");
            Assert.AreNotEqual(Guid.Empty, newMovement.Id);
            Assert.AreEqual(150, newMovement.Amount);
            Assert.AreEqual("Test1", newMovement.Subject);
        }
    }
}
