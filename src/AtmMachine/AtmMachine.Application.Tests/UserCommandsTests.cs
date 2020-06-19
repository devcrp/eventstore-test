using AtmMachine.Application.Services;
using AtmMachine.Domain.Entities;
using AtmMachine.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Application.Tests
{
    public class UserCommandsTests
    {
        EfRepository _EfRepository;

        [SetUp]
        public void Setup()
        {
            _EfRepository = new EfRepository();
        }

        [Test]
        public void Open_Account_Should_Persist_New_Account_And_Movement()
        {
            User newUser = User.Create("John", "Brown");

            OpenAccountCommand command = new OpenAccountCommand(_EfRepository);
            Guid accountId = command.Run(Guid.NewGuid(), 100);

            Account account = _EfRepository.Get<Account>(accountId);

            Assert.NotNull(account);
        }
    }
}
