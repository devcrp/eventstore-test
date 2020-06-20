using AtmMachine.Infrastructure.Contexts.Options;
using EventStore.ClientAPI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine.Infrastructure.Contexts
{
    public class EsConnection
    {
        private readonly IOptions<EsConnectionOptions> _options;

        public IEventStoreConnection Connection { get; set; }

        public EsConnection(IOptions<EsConnectionOptions> options)
        {
            this._options = options;
        }

        public async Task ConnectAsync()
        {
            Connection = await GetConnection(_options.Value.ConnectionString);
        }

        private static async Task<IEventStoreConnection> GetConnection(string connectionString)
        {
            var settingsBuilder = ConnectionSettings
                .Create()
                .EnableVerboseLogging()
                //.UseConsoleLogger()
                .DisableTls();

            IEventStoreConnection conn = EventStoreConnection.Create(connectionString, settingsBuilder);
            await conn.ConnectAsync();
            return conn;
        }
    }
}
