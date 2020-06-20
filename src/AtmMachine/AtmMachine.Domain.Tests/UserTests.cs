using AtmMachine.Domain.Entities;
using AtmMachine.Domain.ValueObjects.Results;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Domain.Tests
{
    public class UserTests
    {
        User _User;

        [SetUp]
        public void Setup()
        {
            _User = User.Create("John", "Brown");
        }

        [Test]
        public void Create_User_Should_Return_Created_User()
        {
            User newUser = User.Create("John", "Brown");
            Assert.AreEqual("John", newUser.Name);
            Assert.AreEqual("Brown", newUser.Surname);
            Assert.AreNotEqual(Guid.Empty, newUser.Id);
        }
        
        [Test]
        public void Open_Account_Should_Link_A_New_Account_To_The_User_And_Have_The_Initial_Movement()
        {
            OpenAccountResult result = _User.OpenAccount(initialDeposit: 100.50m);

            Assert.AreEqual(_User.Id, result.Account.UserId);
            Assert.IsNotNull(result.Movement);
        }
    }
}
