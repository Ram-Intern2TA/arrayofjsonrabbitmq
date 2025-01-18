using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using Newtonsoft.Json;
namespace arrayofjsonrabbitmq
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(
                    queue:"Sample1",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string[] message = { "Abi", "Chennai", "2004" };

                string jsonmessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonmessage);

                await channel.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: "Sample1",
                    body: body
                    );

                Console.WriteLine($"{jsonmessage} , sent successfully");

                Console.ReadLine();
            }

        }
    }
}
