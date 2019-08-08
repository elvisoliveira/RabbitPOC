using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitPOC
{
    public class Receive
    {
        public static void Execute(ConnectionFactory factory)
        {
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            const string queue = "She-Ha";
            channel.QueueDeclare(queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: queue,
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}