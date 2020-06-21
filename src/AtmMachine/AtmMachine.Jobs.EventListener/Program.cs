using AtmMachine.Domain;
using AtmMachine.Jobs.Shared;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AtmMachine.Jobs.EventListener
{
    class Program
    {
        static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            ConsoleColors.SetInitialColors(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine("Starting subscription...");

            _serviceProvider = JobDependencyInjection.Setup();
        }

        static void CreateSubscription()
        {
            IEventRepository eventRepository = _serviceProvider.GetService<IEventRepository>();
            PersistentSubscriptionSettingsBuilder settings = PersistentSubscriptionSettings.Create().DoNotResolveLinkTos().StartFromCurrent();
            try
            {
                await eventRepository.CreatePersistentSubscriptionAsync("entity-1", "gr1", settings, new UserCredentials("admin", "changeit"));
            }
            catch (Exception)
            {
            }
        }
    }
}
