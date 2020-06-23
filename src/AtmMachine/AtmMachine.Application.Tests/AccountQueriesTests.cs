using AtmMachine.Application.Accounts.Queries;
using AtmMachine.Domain.Entities;
using AtmMachine.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Application.Tests
{
    public class AccountQueriesTests
    {
        TestEventsRepository _eventsRepository;

        [SetUp]
        public void Setup()
        {
            _eventsRepository = new TestEventsRepository();
        }

        [Test]
        public async Task Account_Movements_Query_Should_Return_Results()
        {
            Guid accountId = Guid.NewGuid();

            await _eventsRepository.AddEventAsync("account", accountId, "deposit", Movement.Create(accountId, 100, "Test"));

            List<Event<Movement>> queryResult = await new GetAccountMovementsQuery(_eventsRepository).GetAsync(accountId);

            Assert.AreEqual(1, queryResult.Count);
        }
    }
}
