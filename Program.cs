using System;
using RabbitMQ.Client;
using System.Configuration;

namespace RabbitPOC
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                UserName = ConfigurationManager.AppSettings.Get("UserName"),
                Password = ConfigurationManager.AppSettings.Get("Password"),
                VirtualHost = ConfigurationManager.AppSettings.Get("VirtualHost"),
                HostName = ConfigurationManager.AppSettings.Get("HostName"),
                Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"))
            };

            Send.Execute(factory);
            // Receive.Execute(factory);
        }
    }
}
