using AtmMachine.Application.Users.Commands;
using AtmMachine.Domain;
using AtmMachine.Domain.Constants;
using AtmMachine.Domain.Entities;
using AtmMachine.Domain.Services;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AtmMachine.SeedJob
{
    class Program
    {
        static ServiceProvider _serviceProvider;
        static ILogger _logger;

        static async Task Main(string[] args)
        {
            SetupDI();

            Console.Write("Ready to populate data. Continue? (y/n) ");
            bool confirm = Console.ReadLine() == "y";

            if (confirm)
            {
                await SeedData();
            }

            Console.ReadKey();
        }

        static void SetupDI()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(config => config.AddConsole())
                .AddSingleton<IEventRepository, EsRepository>()
                .AddSingleton<EsConnection>()
                .Configure<EsConnectionOptions>(x => x.ConnectionString = "ConnectTo=tcp://admin:changeit@localhost:1113")
                .BuildServiceProvider();

            _logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
        }

        static async Task SeedData()
        {
            Guid accountId = Guid.NewGuid();
            IEventRepository eventRepository = _serviceProvider.GetService<IEventRepository>();
            const int to = 10_000;
            Console.Clear();
            for (int i = 0; i < to; i++)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Inserted {i + 1} of {to}");

                Movement movement = Movement.Create(accountId, GetRandomAmount(), $"Seed Movement {i}");
                await eventRepository.AddEventAsync<Movement>("_" + StreamNameProvider.Get<Account>(accountId),
                                                                EventType.GetByAmount(movement.Amount),
                                                                movement);
            }
        }

        static Random _random = new Random();
        static decimal GetRandomAmount()
        {
            decimal minimum = -500, maximum = 500;
            return (decimal)_random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
