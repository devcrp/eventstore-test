using AtmMachine.Domain.Entities;
using AtmMachine.Domain.ValueObjects;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Infrastructure.Tests
{
    public class EsRepositoryTests
    {
        [Test]
        public async Task Written_Event_Should_Be_Returned_On_Query()
        {
            IOptions<EsConnectionOptions> esOptions = Options.Create<EsConnectionOptions>(new EsConnectionOptions()
            {
                ConnectionString = "ConnectTo=tcp://admin:changeit@localhost:1113"
            });

            EsConnection esConnection = new EsConnection(esOptions);
            await esConnection.ConnectAsync();
            EsRepository esRepository = new EsRepository(esConnection);

            await esRepository.AddEventAsync<Movement>("test-1", "test", Movement.Create(Guid.NewGuid(), 100, "test"));
            List<Event<Movement>> result = await esRepository.GetEventsAsync<Movement>("test-1");

            Assert.AreNotEqual(0, result.Count);
        }
    }
}
