using HomeMQ.RabbitMQ.Consumer;
using RabbitMQ.Client;
using System;

namespace HomeMQ.RabbitMQ.ConsumerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = "devin";
            var password = "Ikorgil19";
            var factory = new ConnectionFactory()
            {
                HostName = "192.168.68.109",
                UserName = username,
                Password = password
            };

            using var conn = factory.CreateConnection();

            var exchangeName = "rtsh_topics";
            var routeKey = "master.control.*";

            var firstConsumer = new MasterControlConsumer(conn, exchangeName, routeKey);
            firstConsumer.Consume();



            Console.ReadLine();
        }
    }
}
