using HomeMQ.Core;
using HomeMQ.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeMQ.RabbitMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.68.109", UserName = "devin", Password = "Ikorgil19" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            GetPiStatusTest(channel);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        static void TopicTest(IModel channel)
        {
            var exchangeName = "topic_logs";
            channel.ExchangeDeclare(exchangeName, type: "topic");
            var routingKey = "control.camera";

            var json = JObject.FromObject(new LightControlMessage());
            var message = json.ToString();
            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.ContentType = "application/json";
            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: props, body: body);

            //channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //var json = JObject.FromObject(new LightControlMessage());

            //string message = json.ToString();

            //var body = Encoding.UTF8.GetBytes(message);
            //var props = channel.CreateBasicProperties();
            //props.ContentType = "application/json";

            //channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: props, body: body, );
            //Console.WriteLine($"[x] Send {message}");

            ////RabbitHutch.CreateBus()
        }

        static void GetPiStatusTest(IModel channel)
        {
            var exchangeName = "rtsh_topics";
            channel.ExchangeDeclare(exchangeName, type: "topic");
            var routingKey = "rasp.control.all";

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };


            var cm = new ControlMessage(new StatusCheck());//JObject.FromObject(new LightControlMessage());
            var message = JsonConvert.SerializeObject(cm, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            

            //var json = JObject.FromObject(cm);
            //var message = json.ToString();
            var body = Encoding.UTF8.GetBytes(message);

            var props = channel.CreateBasicProperties();
            props.ContentType = "application/json";

            while (true)
            {
                //Thread.Sleep(1);
                for (int i = 0; i < 1; i++)
                {
                    channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: props, body: body);
                }
                Thread.Sleep(1000);
                
            }
            

        }
    }
}
