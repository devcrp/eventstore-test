using AtmMachine.Domain;
using AtmMachine.Infrastructure.Contexts;
using AtmMachine.Infrastructure.Contexts.Options;
using AtmMachine.Infrastructure.Repositories;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serializedio.Client;
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

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("secrets.json", optional: false, reloadOnChange: true)
                .Build();

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(config => config.AddConsole())
                .AddSingleton<IEventRepository, SerializedioRepository>()
                .AddSingleton<EsConnection>()
                .Configure<EsConnectionOptions>(x => x.ConnectionString = "ConnectTo=tcp://admin:changeit@localhost:1113")
                .Configure<SerializedClientFactoryOptions>(Configuration.GetSection("serialized"))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
