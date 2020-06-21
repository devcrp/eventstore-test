using AtmMachine.Infrastructure.Contexts.Options;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Principal;
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
            Connection = GetConnection(_options.Value.ConnectionString).Result;
        }

        public UserCredentials GetUserCredentials() => new UserCredentials(_options.Value.User, _options.Value.Password);

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
