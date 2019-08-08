using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitPOC
{
    public class Send
    {
        public static void Execute(ConnectionFactory factory)
        {
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            const string exchange = "He-Man";
            channel.ExchangeDeclare(exchange, ExchangeType.Topic, true, false, null);
            
            const string queue = "She-Ha";
            channel.QueueDeclare(queue, true, false, false, null);

            const string routing = "Esqueleto";
            channel.QueueBind(queue, exchange, routing);

            const string message = "Pelos poderes de Grayskull! Eu tenho a força!";
            var body = Encoding.UTF8.GetBytes(message);
            
            // channel.BasicPublish(exchange, routing, null, body);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.ContentType = "text/plain";
            
            var address = new PublicationAddress(ExchangeType.Topic, exchange, routing);
            channel.BasicPublish(address, properties, body);

            channel.Close();
            connection.Close();
        }
    }
}