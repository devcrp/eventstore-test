using Serializedio.Client;
using Serializedio.Client.Aggregates;
using Serializedio.Client.Projections;
using Serializedio.Client.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serialized_POC
{
    class Program
    {
        static Guid AGGREGATE_ID = Guid.Parse("0f0ad80b-5f8e-405d-91a4-f2ce2c00f2f0");
        static Random _random = new Random();
        static AggregatesClient _aggregatesClient;
        static ProjectionsClient _projectionsClient;

        static async Task Main(string[] args)
        {
            SerializedClientFactory serializedClientFactory = SerializedClientFactory.Create(options =>
                                                              {
                                                                  options.BaseUri = new Uri("https://api.serialized.io/");
                                                                  options.AccessKey = "af022c25705f4d3eb350a17384c32100";
                                                                  options.SecretAccessKey = "56dceaa81e0f41bdb76adc7db20a5b3d9ee90a31cee34d39ac938d2054cfa38c";
                                                              });

            _aggregatesClient = serializedClientFactory.CreateAggregatesClient();
            _projectionsClient = serializedClientFactory.CreateProjectionsClient();

            Console.Write("Run method Read(r), Seed(s), CreateProjection(p), QueryProjection(q): ");
            string method = Console.ReadLine();

            switch (method)
            {
                case "r":
                    await ReadAggregate();
                    break;
                case "s":
                    await Seed();
                    break;
                case "p":
                    await CreateProjection();
                    break;
                case "q":
                    await QueryProjection();
                    break;
                default:
                    Console.WriteLine("Method not found.");
                    break;
            }

            Console.ReadKey();
        }

        static async Task QueryProjection()
        {
            HttpResponseMessage response = await _projectionsClient.QuerySingleAsync("first-projection", AGGREGATE_ID);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        static async Task CreateProjection()
        {
            HttpResponseMessage response = await _projectionsClient.CreateAsync("first-projection", "movement", new ProjectionDefinition
                                            {
                                                EventType = "deposit",
                                                Functions = new List<ProjectionDefinitionFunction>
                                                {
                                                    {
                                                        new ProjectionDefinitionFunction
                                                        {
                                                            Function = "add",
                                                            EventSelector = "$.event.amount",
                                                            TargetSelector = "$.projection.totaAmount"
                                                        }
                                                    }
                                                }
                                            });

            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Request error");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Projection created");
            }
        }

        static async Task Seed()
        {
            for (int i = 0; i < 1_000; i++)
            {
                HttpResponseMessage response = await _aggregatesClient.AddEventAsync("movement", AGGREGATE_ID, new Event<object>()
                {
                    EventId = Guid.NewGuid(),
                    EventType = "deposit",
                    Data = new { amount = _random.Next(1, 100) }
                });

                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Request error");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.WriteLine($"{i}. Event added.");
                }
            }
        }

        static async Task ReadAggregate()
        {
            HttpResponseMessage getResponse = await _aggregatesClient.GetAsync("movement", AGGREGATE_ID);
            Console.WriteLine(await getResponse.Content.ReadAsStringAsync());
        }
    }
}
