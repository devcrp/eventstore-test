using EventStore.ClientAPI;
using EventStore.ClientAPI.Exceptions;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventStore_POC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Connecting to EventStore...");
            IEventStoreConnection conn = await GetEventStoreConnection();
            Console.WriteLine("Connected!");

            //foreach (string f in Directory.GetFiles("../../../../sample-data/", "shoppingCart-*"))
            //{
            //    var streamName = Path.GetFileNameWithoutExtension(f);

            //    var step3EventData = JsonConvert.DeserializeObject<List<EventDataWrapper>>(File.ReadAllText(f)).Select(x => x.ToEventData());
            //    var eventData = step3EventData.ToArray();
            //    await conn.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
            //}

            do
            {
                Console.Write("Want to write (w), read (r) or subscribe(s)? ");
                string option = Console.ReadLine();

                if (option == "w")
                {
                    Console.Write("value: ");
                    string value = Console.ReadLine();

                    Console.WriteLine("Writting new event...");
                    await WriteEvent(conn, value);
                    Console.WriteLine("Event written!");
                }
                else if (option == "r")
                {
                    //var readResult = await esConnection.ReadEventAsync("entity-1", 0, true);
                    //Console.WriteLine(Encoding.UTF8.GetString(readResult.Event.Value.Event.Data));

                    var readEvents = await conn.ReadStreamEventsForwardAsync("entity-1", 0, 10, true);
                    foreach (var evt in readEvents.Events)
                        Console.WriteLine(evt.Event.EventType + " - " + Encoding.UTF8.GetString(evt.Event.Data));
                }
                else
                {
                    var settings = PersistentSubscriptionSettings.Create().DoNotResolveLinkTos().StartFromCurrent();
                    try
                    {
                        await conn.CreatePersistentSubscriptionAsync("entity-1", "gr1", settings, new UserCredentials("admin", "changeit"));
                    }
                    catch(Exception)
                    { 
                    }
                    EventStorePersistentSubscriptionBase subscription = await conn.ConnectToPersistentSubscriptionAsync(
                                        "entity-1", "gr1", (_, evt) =>
                                        {
                                            Console.WriteLine("Received: " + Encoding.UTF8.GetString(evt.Event.Data));
                                        });
                    Console.Clear();
                    Console.WriteLine("Listening to subscription...");
                    Console.ReadKey();
                }

                Console.WriteLine("--------------------------");
                Console.WriteLine("");

            } while (true);
        }

        static async Task WriteEvent(IEventStoreConnection conn, string value)
        {
            string streamName = "entity-1";
            string eventType = "edit";
            string data = "{ \"a\":\"" + value + "\"}";
            string metadata = "{}";
            EventData eventPayload = new EventData(Guid.NewGuid(), eventType, true, Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(metadata));
            await conn.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventPayload);
        }

        static async Task<IEventStoreConnection> GetEventStoreConnection()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var connectionString = $"ConnectTo=tcp://admin:changeit@localhost:1113";

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
