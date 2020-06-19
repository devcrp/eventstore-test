using AtmMachine.Application.Services;
using AtmMachine.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmMachine.Application.Tests
{
    public class UserServiceTests
    {
        OpenAccountCommand _UserService;

        [SetUp]
        public void Setup()
        {
            _UserService = new OpenAccountCommand(new EfRepository());
        }

        [Test]
        public void Open_Account_Should_Persist_New_Account_And_Movement()
        {
            _UserService.
        }
    }
}
