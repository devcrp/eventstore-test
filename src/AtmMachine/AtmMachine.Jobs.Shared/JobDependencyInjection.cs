using AtmMachine.Domain;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AtmMachine.Jobs.Shared
{
    public static class JobDependencyInjection
    {
        public static ServiceProvider Setup()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(config => config.AddConsole())
                .AddSingleton<IEventRepository, EsRepository>()
                .AddSingleton<EsConnection>()
                .Configure<EsConnectionOptions>(x => x.ConnectionString = "ConnectTo=tcp://admin:changeit@localhost:1113")
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
