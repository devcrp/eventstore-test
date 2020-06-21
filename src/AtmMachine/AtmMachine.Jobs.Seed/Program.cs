using AtmMachine.Application.Users.Commands;
using AtmMachine.Domain;
using AtmMachine.Domain.Constants;
using AtmMachine.Domain.Entities;
using AtmMachine.Domain.Services;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using AtmMachine.Jobs.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace AtmMachine.Jobs.Seed
{
    class Program
    {
        static ServiceProvider _serviceProvider;
        static ILogger _logger;
        const int ITEMS_TO_ADD = 10_000;

        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ConsoleColors.SetInitialColors(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("Starting...");
            _serviceProvider = JobDependencyInjection.Setup();

            _logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            await SeedData();

            Console.WriteLine($"Completed in {Math.Round((double)stopwatch.ElapsedMilliseconds / 1_000, 2)}s");
            Console.ReadKey();
        }

        static async Task SeedData()
        {
            Guid accountId = Guid.NewGuid();
            IEventRepository eventRepository = _serviceProvider.GetService<IEventRepository>();
            Console.Clear();
            for (int i = 0; i < ITEMS_TO_ADD; i++)
            {

                double d = (double)((((decimal)i + 1) / (decimal)ITEMS_TO_ADD) * 100);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Inserted {i + 1} of {ITEMS_TO_ADD} --> {Math.Round(d, 2)}% ");

                Movement movement = Movement.Create(accountId, GetRandomAmount(), $"Seed Movement {i}");
                await eventRepository.AddEventAsync<Movement>(StreamNameProvider.Get<Account>(accountId),
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
