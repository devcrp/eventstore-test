using AtmMachine.Application.Users.Commands;
using AtmMachine.Domain.Entities;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.EfContext;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Application.Tests
{
    public class UserCommandsTests
    {
        EfRepository _EfRepository;
        TestEventsRepository _EsRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<EfContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EfContext context = new EfContext(options);
            _EfRepository = new EfRepository(context);

            _EsRepository = new TestEventsRepository();
        }

        [Test]
        public void Create_User_Command_Should_Persist_New_User()
        {
            CreateUserCommand command = new CreateUserCommand(_EfRepository);
            Guid userId = command.Run("John", "Brown");

            User user = _EfRepository.Get<User>(userId);

            Assert.IsNotNull(user);
        }

        [Test]
        public async Task Open_Account_Command_Should_Persist_New_Account_And_Movement()
        {
            CreateUserCommand createUserCommand = new CreateUserCommand(_EfRepository);
            Guid userId = createUserCommand.Run("John", "Brown");

            OpenAccountCommand command = new OpenAccountCommand(_EfRepository, _EsRepository);

            Guid accountId = await command.RunAsync(userId, 100);

            Account account = _EfRepository.Get<Account>(accountId);

            Assert.NotNull(account);
        }
    }
}
